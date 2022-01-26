using System;
using System.Collections.Generic;
using System.Linq;
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
        public int OvertimeRequest(OvertimeRequestVM overtimeRequestVM)
        {
            var os = new OvertimeSchedule
            {
                StartDate = overtimeRequestVM.StartDate,
                EndDate = overtimeRequestVM.EndDate,
                Note = overtimeRequestVM.Note

            };
            myContext.OvertimesSchedules.Add(os);
            myContext.SaveChanges();
            var eos = new EmployeeOvertimeSchedule
            {
                OvertimeSchedule_ID = os.OvertimeSchedule_ID,
                NIK = overtimeRequestVM.NIK,
            };
            myContext.EmployeeOvertimeSchedules.Add(eos);
            myContext.SaveChanges();
            return 1;
        }
    }
}
