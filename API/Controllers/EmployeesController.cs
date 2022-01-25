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
            if (register == 0)
            {
                return Ok(register);
            }
            else if (register == 1)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, register, message = "Email Duplicate" });
            }
            else if (register == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, register, message = "Phone Duplicate" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Can't Insert Data, NIK Duplicate" }); ;
            }
        }
    }
}
