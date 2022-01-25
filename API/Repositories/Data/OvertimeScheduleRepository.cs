using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class OvertimeScheduleRepository : GeneralRepository<MyContext, OvertimeSchedule, int>
    {
        public OvertimeScheduleRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
