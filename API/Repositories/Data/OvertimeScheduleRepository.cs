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
                
                var fromAddress = new MailAddress("mcc.61.2021@gmail.com");
                var passwordFrom = "kahardian61";
                var toAddress = new MailAddress(findManagerAcc.Email);
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, passwordFrom)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Overtime Request",
                    Body = "Hai",
                    IsBodyHtml = true,
                }) smtp.Send(message);
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.NotFound;
            }
        }
    }
}
