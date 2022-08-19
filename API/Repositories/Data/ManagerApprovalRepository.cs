using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class ManagerApprovalRepository : GeneralRepository<MyContext, ManagerApproval, int>
    {
        private readonly MyContext myContext;
        public ManagerApprovalRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public HttpStatusCode Approval(ManagerApproval managerApproval)
        {
            //var findOvertimeSchedule = myContext.OvertimesRequests.Include(os => os.Employee).Where(e => e.OvertimeRequestID == ID).FirstOrDefault();
            var findOvertimeSchedule = myContext.OvertimesRequests.Where(or => or.OvertimeRequestID == managerApproval.OvertimeRequestID).FirstOrDefault();
            var findAccount = myContext.Employees.Where(e => e.NIK == findOvertimeSchedule.NIK).Include(e=>e.Account).SingleOrDefault();
            try
            {
                var result = new ManagerApproval
                {
                    ManagerApprovalStatus = managerApproval.ManagerApprovalStatus,
                    OvertimeRequestID = findOvertimeSchedule.OvertimeRequestID,
                    ManagerNote = managerApproval.ManagerNote
                };
                myContext.ManagerApprovals.Add(result);
                myContext.SaveChanges();
                try
                {
                    var toEmail = findAccount.Account.Email;
                    var subjectEmail = "Your Request Get A Respon From Manager";
                    var bodyEmail = "";
                    if (result.ManagerApprovalStatus == ManagerApprovalStatus.Accepted)
                    {
                        bodyEmail = "Your request has been accepted. Now, just waiting for the approval of the finance team. We'll let you know soon";
                    }
                    else
                    {
                        bodyEmail = "Your request has been rejected. Contact the manager if you has a question";
                    }
                    OvertimeRequestRepository.SendEmail(toEmail, subjectEmail, bodyEmail);
                }
                catch(Exception )
                {
                    throw;
                }
                return HttpStatusCode.OK;
            }
            catch(Exception )
            {
                return HttpStatusCode.BadRequest;
            }
        }


    }
}
