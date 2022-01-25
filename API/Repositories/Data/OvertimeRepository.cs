using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class OvertimeRepository : GeneralRepository<MyContext, Overtime, string>
    {
        public OvertimeRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
