using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class OvertimeBonusRepository : GeneralRepository<MyContext, OvertimeBonus, int>
    {
        public OvertimeBonusRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
