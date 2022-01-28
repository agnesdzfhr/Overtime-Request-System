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
    public class OvertimeScheduleRepository : GeneralRepository<MyContext, OvertimeSchedule, int>
    {
        public readonly MyContext myContext;
        public OvertimeScheduleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public HttpStatusCode OvertimeRequest(OvertimeRequestVM overtimeRequestVM)
        {
            var findEmployee = myContext.Employees.Where(e => e.NIK == overtimeRequestVM.NIK).FirstOrDefault();
            var findManager = myContext.Employees.Where(e => e.NIK == findEmployee.Manager_ID).FirstOrDefault();

            List<OvertimeSchedule> os = overtimeRequestVM.OvertimeSchedules;
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

        public IEnumerable<Object> GetForManager(string NIK)
        {
            //var findManager = myContext.Employees.Where( m => m.NIK == NIK).FirstOrDefault();
            var findListEmployee = myContext.OvertimesSchedules.Include(os => os.Employee).Where(e => e.Employee.Manager_ID == NIK).ToList();

            var listResult = new List<OvertimeSchedulesVM>();
            foreach (var item in findListEmployee)
            {
                var result = new OvertimeSchedulesVM
                {
                    NIK = item.NIK,
                    FullName = $"{item.Employee.FirstName} {item.Employee.LastName}",
                    Date = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Note = item.Note

                };

                listResult.Add(result);
            }

            return listResult;
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
