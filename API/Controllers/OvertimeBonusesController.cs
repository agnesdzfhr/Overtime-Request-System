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
    public class OvertimeBonusesController : BaseController<OvertimeBonus, OvertimeBonusRepository, int>
    {
        private readonly OvertimeBonusRepository overtimeBonusRepository;
        public OvertimeBonusesController(OvertimeBonusRepository overtimeBonusRepository) : base(overtimeBonusRepository)
        {
            this.overtimeBonusRepository = overtimeBonusRepository;
        }
    }
}
