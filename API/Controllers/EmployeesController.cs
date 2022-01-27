using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Base;
using API.Context;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.ViewModel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly MyContext myContext;
        public EmployeesController(EmployeeRepository employeeRepository, MyContext myContext) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.myContext = myContext;
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var register = employeeRepository.Register(registerVM);
            switch (register)
            {
                case HttpStatusCode.OK:
                    return Ok(register);
                case HttpStatusCode.Conflict:
                    return Ok(new { status = register, message = "Email Already Used" });
                default:
                    return Ok(new { status = register, message = "Phone Already Used" });
            }
        }

        [HttpGet]
        [Route("GetRegisterData/{nik}")]
        public ActionResult<Object> GetRegisterData(string NIK)
        {
            try
            {
                var getRegisterData = employeeRepository.GetRegisteredData(NIK);
                if (getRegisterData != null)
                {
                    //return Ok(new{ message="Data Found",data=getRegisterData});
                    return getRegisterData;
                }
                return getRegisterData;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
