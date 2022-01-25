using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class EmployeeOvertimeScheduleRepository : GeneralRepository<MyContext, EmployeeOvertimeSchedule, int>
    {
        public EmployeeOvertimeScheduleRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
