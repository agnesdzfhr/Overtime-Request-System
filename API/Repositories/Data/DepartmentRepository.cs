using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class DepartmentRepository : GeneralRepository<MyContext, Department, string>
    {
        public DepartmentRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
