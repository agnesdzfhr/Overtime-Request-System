using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Base;
using API.Context;
using API.Models;
using API.Repositories.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeSchedulesController : BaseController<OvertimeSchedule, OvertimeScheduleRepository, int>
    {
        private readonly OvertimeScheduleRepository overtimeScheduleRepository;
        private readonly MyContext myContext;
        public OvertimeSchedulesController(OvertimeScheduleRepository overtimeScheduleRepository, MyContext myContext) : base(overtimeScheduleRepository)
        {
            this.overtimeScheduleRepository = overtimeScheduleRepository;
            this.myContext = myContext;
        }
        [HttpPost("OvertimeRequest")]
        public ActionResult OvertimeRequest(OvertimeRequestVM overtimeRequestVM)
        {
            var request = overtimeScheduleRepository.OvertimeRequest(overtimeRequestVM);
            try
            {
                if (request == 1)
                {
                    return Ok(HttpStatusCode.OK);
                }
                return Ok(HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
