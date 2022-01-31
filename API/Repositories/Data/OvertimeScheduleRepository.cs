using System;
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
    public class OvertimeScheduleRepository : GeneralRepository<MyContext, OvertimeRequest, int>
    {
        public readonly MyContext myContext;
        public OvertimeScheduleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public HttpStatusCode OvertimeRequest(OvertimeRequestVM overtimeRequestVM)
        {
            var findEmployee = myContext.Employees.Where(e => e.NIK == overtimeRequestVM.NIK).FirstOrDefault();
            var findManager = myContext.Employees.Where(e => e.NIK == findEmployee.ManagerID).FirstOrDefault();

            List<OvertimeRequest> os = overtimeRequestVM.OvertimeSchedules;
            foreach (var item in os)
            {
                item.NIK = overtimeRequestVM.NIK;
            }
            myContext.OvertimesSchedules.AddRange(os);
            myContext.SaveChanges();

            if (findManager != null)
            {
                var findManagerAcc = myContext.Accounts.Where(a => a.NIK == findManager.NIK).FirstOrDefault();

                var toEmail = findManagerAcc.Email;
                var subjectEmail = "Overtime Request";
                var bodyEmail = "Hai, Ini hanya email uji coba";

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
            var findListEmployee = myContext.OvertimesSchedules.Include(os => os.Employee).Where(e => e.Employee.ManagerID == NIK).ToList();

            var listResult = new List<OvertimeSchedulesVM>();
            foreach (var item in findListEmployee)
            {
                var result = new OvertimeSchedulesVM
                {
                    OvertimeSchedule_ID = item.OvertimeRequestID,
                    NIK = item.NIK,
                    FullName = $"{item.Employee.FirstName} {item.Employee.LastName}",
                    Date = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Note = item.JobNote

                };

                listResult.Add(result);
            }

            var listResultDescending = listResult.OrderByDescending(lr => lr.OvertimeSchedule_ID).ToList();

            return listResultDescending;
        }

        public OvertimeSchedulesVM GetOvertimeScheduleByID(int ID)
        {
            var findOvertimeSchedule = myContext.OvertimesSchedules.Include(os => os.Employee).Where(e => e.OvertimeRequestID == ID).FirstOrDefault();
                var result = new OvertimeSchedulesVM
                {
                    OvertimeSchedule_ID = findOvertimeSchedule.OvertimeRequestID,
                    NIK = findOvertimeSchedule.NIK,
                    FullName = $"{findOvertimeSchedule.Employee.FirstName} {findOvertimeSchedule.Employee.LastName}",
                    Date = findOvertimeSchedule.Date,
                    StartTime = findOvertimeSchedule.StartTime,
                    EndTime = findOvertimeSchedule.EndTime,
                    Note = findOvertimeSchedule.JobNote

                };
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
    }
}
