using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Http;

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
            //var findOvertimeType = myContext.Overtimes.Select(o => o.Overtime_ID).FirstOrDefault();

            List<OvertimeSchedule> os = overtimeRequestVM.OvertimeSchedules;
            myContext.OvertimesSchedules.AddRange(os);
            myContext.SaveChanges();
            List<int> osIdList =  os.Select(os => os.OvertimeSchedule_ID).ToList() ;
            foreach (var id in osIdList)
            {
                var osId = new EmployeeOvertimeSchedule
                {
                    OvertimeSchedule_ID = id,
                    NIK = overtimeRequestVM.NIK
                };
                myContext.EmployeeOvertimeSchedules.Add(osId);
            }
            myContext.SaveChanges();
            return HttpStatusCode.OK;
        }
    }
}
