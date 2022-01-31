using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Context;
using API.Models;

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
            try
            {
                var result = new ManagerApproval
                {
                    ManagerApprovalStatus = managerApproval.ManagerApprovalStatus,
                    OvertimeRequestID = findOvertimeSchedule.OvertimeRequestID
                };
                myContext.ManagerApprovals.Add(result);
                myContext.SaveChanges();
                return HttpStatusCode.OK;
            }
            catch(Exception e)
            {
                return HttpStatusCode.BadRequest;
            }
        }


    }
}
