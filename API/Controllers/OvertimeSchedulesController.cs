using System;
using System.Collections.Generic;
using System.Linq;
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
    public class OvertimeSchedulesController : BaseController<OvertimeSchedule, OvertimeScheduleRepository, int>
    {
        private readonly OvertimeScheduleRepository overtimeScheduleRepository;
        public OvertimeSchedulesController(OvertimeScheduleRepository overtimeScheduleRepository) : base(overtimeScheduleRepository)
        {
            this.overtimeScheduleRepository = overtimeScheduleRepository;
        }
    }
}
