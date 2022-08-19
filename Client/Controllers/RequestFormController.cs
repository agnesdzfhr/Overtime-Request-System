using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Client.Base;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class RequestFormController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository repository;
        public RequestFormController(EmployeeRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Employees/GetData")]
        public async Task<JsonResult> GetData()
        {
            var NIK = HttpContext.Session.GetString("NIK");
            var result = await repository.GetRegisterByNIK(NIK);
            return Json(result);
        }

        [HttpGet("Employees/GetOvertimeHistory")]
        public async Task<JsonResult> GetOvertimeHistory()
        {
            var NIK = HttpContext.Session.GetString("NIK");
            var result = await repository.GetOvertimeHistory(NIK);
            return Json(result);
        }
    }
}
