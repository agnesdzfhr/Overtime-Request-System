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
    public class OvertimesController : BaseController<Overtime, OvertimeRepository, string>
    {
        private readonly OvertimeRepository overtimeRepository;
        public OvertimesController(OvertimeRepository overtimeRepository) : base(overtimeRepository)
        {
            this.overtimeRepository = overtimeRepository;
        }
    }
}
