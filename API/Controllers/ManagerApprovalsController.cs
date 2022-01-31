using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Base;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerApprovalsController : BaseController<ManagerApproval, ManagerApprovalRepository, int>
    {
        private readonly ManagerApprovalRepository managerApprovalRepository;
        public ManagerApprovalsController(ManagerApprovalRepository managerApprovalRepository) : base(managerApprovalRepository)
        {
            this.managerApprovalRepository = managerApprovalRepository;
        }

        [HttpPost("Approval")]
        public ActionResult Approval(ManagerApproval managerApproval)
        {
            var register = managerApprovalRepository.Approval(managerApproval);
            switch (register)
            {
                case HttpStatusCode.OK:
                    return Ok(register);
                default:
                    return BadRequest(new { status = register, message = "Overtime Request ID Note Found" });
            }
        }
    }
}
