using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<Object> GetRegisteredData()
        {
            var query = from emp in myContext.Employees
                        join dp in myContext.Departments
                          on emp.DepartmentID equals dp.DepartmentID
                        join acc in myContext.Accounts
                          on emp.NIK equals acc.NIK
                        join accr in myContext.AccountRoles
                          on acc.AccountID equals accr.AccountID
                        select new
                        {
                            NIK = emp.NIK,
                            FullName = emp.FirstName + " " + emp.LastName,
                            PhoneNumber = emp.Phone,
                            Gender = emp.Gender.ToString(),
                            Email = acc.Email,
                            Salary = emp.Salary,
                            Department_ID = dp.DepartmentID 
                        };
            return query;
        }

        public RegisterVM GetRegisteredData(string NIK)
        {
            var query = myContext.Employees.Where(e => e.NIK == NIK)
                                        .Include(e=>e.Department)
                                        .Include(e => e.Account)
                                        .ThenInclude(a => a.AccountRoles)
                                        .ThenInclude(ar => ar.Role)
                                        .FirstOrDefault();

            if (query == null)
            {
                return null;
            }

            var grd = new RegisterVM
            {
                NIK = query.NIK,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Gender = query.Gender,
                Phone = query.Phone,
                Salary = query.Salary,
                //Gender = RegisterVM.GetGender((int)e.Gender),
                Department = query.Department.Name,
                Email = query.Account.Email,
                //Password = query.Account.Password,
                Role = query.Account.AccountRoles.Where(ar => ar.AccountID == query.Account.AccountID).Select(ar => ar.Role.Name).ToList()


            };

            return grd;
        }


        public HttpStatusCode Register(RegisterVM registerVM)//bikin pengecekan email dan nomorHp
        {
            var checkEmail = myContext.Accounts.Any(x => x.Email == registerVM.Email);
            var checkPhone = myContext.Employees.Any(x => x.Phone == registerVM.Phone);
            var incrementEmp = myContext.Employees.ToList().Count;
            var incrementAcc = myContext.Accounts.ToList().Count;
            var formattedNIK = "";
            if (incrementEmp == 0)
            {
                formattedNIK = DateTime.Now.Year.ToString() + "0" + (incrementEmp + 1).ToString();

            }
            else
            {
                var incrementEmp2 = myContext.Employees.ToList().Max(e => e.NIK);
                formattedNIK = (Int32.Parse(incrementEmp2) + 1).ToString();

            }
            var formattedAccID = "";
            if (incrementAcc == 0)
            {
                formattedAccID = 1000 + (incrementAcc + 1).ToString();

            }
            else
            {
                var incrementAcc2 = myContext.Accounts.ToList().Max(a=> a.AccountID);
                formattedAccID = (Int32.Parse(incrementAcc2) + 1).ToString();

            }
            if (checkEmail)
            {
                return HttpStatusCode.Conflict;
            }
            else if (checkPhone)
            {
                return HttpStatusCode.BadRequest;
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
                    WorkHourPerDay = 8,
                    WorkDayPerMonth = 20,
                    Gender = registerVM.Gender,
                    DepartmentID = registerVM.Department,
                    OvertimeLimitID = "OL01"
                };
                myContext.Employees.Add(emp);
                myContext.SaveChanges();
                var acc = new Account
                {
                    AccountID = formattedAccID,
                    NIK = emp.NIK,
                    Email = registerVM.Email,
                    Password = Hashing.HashPassword(registerVM.Password)
                };
                myContext.Accounts.Add(acc);
                myContext.SaveChanges();
                var accountRole = new AccountRole
                {
                    AccountID = acc.AccountID,
                    RoleID = "R02"
                };
                myContext.AccountRoles.Add(accountRole);
                myContext.SaveChanges();
                return HttpStatusCode.OK;
            }
        }
        
    }
}
