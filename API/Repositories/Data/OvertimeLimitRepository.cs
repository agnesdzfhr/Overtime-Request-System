using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class OvertimeLimitRepository : GeneralRepository<MyContext, OvertimeLimit, string>
    {
        public OvertimeLimitRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
