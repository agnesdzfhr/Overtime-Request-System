using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModel;
using static API.Repositories.Data.AccountRepository;

namespace API.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        public readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int Register(RegisterVM registerVM)//bikin pengecekan email dan nomorHp
        {
            var checkEmail = myContext.Accounts.Any(x => x.Email == registerVM.Email);
            var checkPhone = myContext.Employees.Any(x => x.Phone == registerVM.Phone);
            var increamentEmp = myContext.Employees.ToList().Count;
            var increamentAcc = myContext.Accounts.ToList().Count;
            var formattedNIK = "";
            if (increamentEmp == 0)
            {
                formattedNIK = DateTime.Now.Year.ToString() + "0" + (increamentEmp + 1).ToString();

            }
            else
            {
                var increamentEmp2 = myContext.Employees.ToList().Max(e => e.NIK);
                formattedNIK = (Int32.Parse(increamentEmp2) + 1).ToString();

            }
            var formattedAccID = "";
            if (increamentAcc == 0)
            {
                formattedAccID = "A" + "0" + (increamentAcc + 1).ToString();

            }
            else
            {
                var increamentAcc2 = myContext.Employees.ToList().Max(e => e.NIK);
                formattedAccID = (Int32.Parse(increamentAcc2) + 1).ToString();

            }
            if (checkEmail)
            {
                return 1;
            }
            else if (checkPhone)
            {
                return 2;
            }
            else
            {
                var emp = new Employee
                {
                    NIK = formattedNIK, //bikin variable biar jadi NIK = formatedNIK
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Phone = registerVM.Phone,
                    Salary = registerVM.Salary,
                    Gender = registerVM.Gender,
                    Department_ID = registerVM.Department_ID
                };
                myContext.Employees.Add(emp);
                myContext.SaveChanges();
                var acc = new Account
                {
                    Account_ID = formattedAccID,
                    NIK = emp.NIK,
                    Email = registerVM.Email,
                    Password = Hashing.HashPassword(registerVM.Password)
                };
                myContext.Accounts.Add(acc);
                myContext.SaveChanges();
                var accountRole = new AccountRole
                {
                    Account_ID = acc.Account_ID,
                    Role_ID = "R02"
                };
                myContext.AccountRoles.Add(accountRole);
                myContext.SaveChanges();
                return 0;
            }
        }
    }
}
