﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class OvertimeRequestRepository : GeneralRepository<MyContext, OvertimeRequest, int>
    {
        public readonly MyContext myContext;
        public OvertimeRequestRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public HttpStatusCode RequestForm(OvertimeRequestVM overtimeRequestVM)
        {
            var findEmployee = myContext.Employees.Where(e => e.NIK == overtimeRequestVM.NIK).FirstOrDefault();
            var findManager = myContext.Employees.Where(e => e.NIK == findEmployee.ManagerID).FirstOrDefault();



            if (findManager != null)
            {
                List<OvertimeRequest> os = overtimeRequestVM.OvertimeRequests;
                foreach (var item in os)
                {
                    item.NIK = overtimeRequestVM.NIK;
                }
                myContext.OvertimesRequests.AddRange(os);
                myContext.SaveChanges();
                var findManagerAcc = myContext.Accounts.Where(a => a.NIK == findManager.NIK).FirstOrDefault();

                var toEmail = findManagerAcc.Email;
                var subjectEmail = "Employee Overtime Request";
                var bodyEmail = "Hi, you has overtime request from your employee, please check your account for more detail";

                SendEmail(toEmail, subjectEmail, bodyEmail);

                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.NotFound;
            }
        }

        public IEnumerable<OvertimeSchedulesVM> GetForManager(string NIK)
        {
            //var findManager = myContext.Employees.Where( m => m.NIK == NIK).FirstOrDefault();
            var findListEmployee = myContext.OvertimesRequests.Include(os => os.Employee).Where(e => e.Employee.ManagerID == NIK).ToList();

            var listResult = new List<OvertimeSchedulesVM>();
            foreach (var item in findListEmployee)
            {
                var findApproval = myContext.ManagerApprovals.Where(ma => ma.OvertimeRequestID == item.OvertimeRequestID).Select(ma => ma.ManagerApprovalStatus).FirstOrDefault();
                var result = new OvertimeSchedulesVM
                {
                    OvertimeSchedule_ID = item.OvertimeRequestID,
                    NIK = item.NIK,
                    FullName = $"{item.Employee.FirstName} {item.Employee.LastName}",
                    Date = item.Date,
                    DateStr = item.Date.ToString("yyyy-MM-dd"),
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    JobNote = item.JobNote,
                    ApprovalStatus = OvertimeSchedulesVM.GetApprovalStatus((int)findApproval)

                };

                listResult.Add(result);
            }

            var listResultDescending = listResult.OrderByDescending(lr => lr.OvertimeSchedule_ID).ToList();

            return listResult;
        }

        public OvertimeSchedulesVM GetOvertimeRequestByID(int ID)
        {
            var findOvertimeSchedule = myContext.OvertimesRequests.Include(os => os.Employee).Where(e => e.OvertimeRequestID == ID).FirstOrDefault();
            var result = new OvertimeSchedulesVM();


            result.OvertimeSchedule_ID = findOvertimeSchedule.OvertimeRequestID;
            result.NIK = findOvertimeSchedule.NIK;
            result.FullName = $"{findOvertimeSchedule.Employee.FirstName} {findOvertimeSchedule.Employee.LastName}";
            result.Date = findOvertimeSchedule.Date;
            result.DateStr = findOvertimeSchedule.Date.ToString("yyyy-MM-dd");
            result.StartTime = findOvertimeSchedule.StartTime;
            result.EndTime = findOvertimeSchedule.EndTime;
            result.JobNote = findOvertimeSchedule.JobNote;
                    
                
            return result;
        }

        public static void SendEmail(string toEmail, string subjectEmail, string bodyEmail)
        {
            string to = toEmail;
            string from = "mccreg61net@gmail.com";

            MailMessage message = new MailMessage(from, to);

            message.Subject = subjectEmail;
            message.Body = bodyEmail;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(from, "61mccregnet"),
                EnableSsl = true
            };
            try
            {
                client.Send(message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Object> GetRegisteredRequest()
        {
            float totalFee = 0;
            var listRequest = myContext.ManagerApprovals
                .Where(ma => ma.ManagerApprovalStatus == ManagerApprovalStatus.Accepted)
                .Include(ma => ma.OvertimeRequest)
                .ThenInclude(or => or.Employee).ToList();

            var firstHour = myContext.OvertimeBonuses.Where(ob => ob.OvertimeBonusID == "OB01").Select(ob => ob.CommisionPct).FirstOrDefault();
            var nextHour = myContext.OvertimeBonuses.Where(ob => ob.OvertimeBonusID == "OB02").Select(ob => ob.CommisionPct).FirstOrDefault();

            var result = new List<OvertimeFinanceApprovalVM>();

            foreach (var item in listRequest)
            {
                var findFinanceValidation = myContext.FinanceValidations.Where(fv => fv.OvertimeRequestID == item.OvertimeRequestID).FirstOrDefault();

                var salaryEmployee = item.OvertimeRequest.Employee.Salary;
                var workDayPerMonth = item.OvertimeRequest.Employee.WorkDayPerMonth;
                var workHourPerDay = item.OvertimeRequest.Employee.WorkHourPerDay;
                var salaryPerHour = salaryEmployee / (workDayPerMonth * workHourPerDay);

                var endTime = item.OvertimeRequest.EndTime;
                TimeSpan et = TimeSpan.Parse(endTime);
                var startTime = item.OvertimeRequest.StartTime;
                TimeSpan st = TimeSpan.Parse(startTime);

                var jamOvertime = et - st;
                var jamOvertimeString = jamOvertime.TotalSeconds.ToString();
                var jamOvertimeInt = int.Parse(jamOvertimeString);
                OvertimeFinanceApprovalVM query = null;
                if (jamOvertimeInt == 3600)
                {
                    totalFee = salaryPerHour * firstHour;
                    query = countBonus(totalFee, item, findFinanceValidation);
                }
                else
                {
                    totalFee = (salaryPerHour * firstHour) + (nextHour * salaryPerHour * ((jamOvertimeInt - 3600)/3600));
                    query = countBonus(totalFee, item, findFinanceValidation);
                }
                result.Add(query);
            }
            return result;

        }

        private static OvertimeFinanceApprovalVM countBonus(float totalFee, ManagerApproval item, FinanceValidation financeValidation)
        {
            var query = new OvertimeFinanceApprovalVM
            {
                NIK = item.OvertimeRequest.NIK,
                FullName = item.OvertimeRequest.Employee.FirstName + " " + item.OvertimeRequest.Employee.LastName,
                Date = item.OvertimeRequest.Date,
                StartTime = item.OvertimeRequest.StartTime,
                EndTime = item.OvertimeRequest.EndTime,
                JobNote = item.OvertimeRequest.JobNote,
                Salary = item.OvertimeRequest.Employee.Salary,
                TotalFee = totalFee,
                OvertimeRequestID = item.OvertimeRequest.OvertimeRequestID,
            };

            if (financeValidation != null)
            {
                query.FinanceApproval = "Completed";
            }
            else
            {
                query.FinanceApproval = "Need Approval";
            }

            return query;
        }
        public HttpStatusCode InsertCountBonus(FinanceValidation financeValidation)
        {
            var checkOvertimeReqID = myContext.OvertimesRequests.Where(or => or.OvertimeRequestID == financeValidation.OvertimeRequestID).FirstOrDefault();
            var findEmail = myContext.Employees.Where(e => e.NIK == checkOvertimeReqID.NIK).Include(e => e.Account).FirstOrDefault();

            if (checkOvertimeReqID == null)
            {
                return HttpStatusCode.BadRequest;
            }
            else
            {
                var financeBonus = new FinanceValidation
                {
                    OvertimeRequestID = checkOvertimeReqID.OvertimeRequestID,
                    TotalFee = financeValidation.TotalFee,
                };
                myContext.FinanceValidations.Add(financeBonus);
                myContext.SaveChanges();

                var toEmail = findEmail.Account.Email;
                var subjectEmail = "Finance Validation";
                var bodyEmal = "Finance has been validate your request";

                SendEmail(toEmail, subjectEmail, bodyEmal);

                return HttpStatusCode.OK;
            }
        }

    }
}
