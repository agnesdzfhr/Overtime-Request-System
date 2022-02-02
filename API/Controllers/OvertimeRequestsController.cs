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
    public class OvertimeRequestsController : BaseController<OvertimeRequest, OvertimeRequestRepository, int>
    {
        private readonly OvertimeRequestRepository overtimeRequestRepository;
        private readonly MyContext myContext;
        public OvertimeRequestsController(OvertimeRequestRepository overtimeScheduleRepository, MyContext myContext) : base(overtimeScheduleRepository)
        {
            this.overtimeRequestRepository = overtimeScheduleRepository;
            this.myContext = myContext;
        }
        [HttpPost("RequestForm")]
        public ActionResult RequestForm(OvertimeRequestVM overtimeRequestVM)
        {
            var request = overtimeRequestRepository.RequestForm(overtimeRequestVM);
            try
            {

                return Ok(request);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetForManager/{nik}")]
        public ActionResult GetForManager(string NIK)
        {
            var response = overtimeRequestRepository.GetForManager(NIK);
            try
            {
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetOvertimeRequestByID/{id}")]
        public ActionResult GetOvertimeRequestByID(int ID)
        {
            var response = overtimeRequestRepository.GetOvertimeRequestByID(ID);
            try
            {
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetRegisteredRequest")]
        public ActionResult GetRegisteredRequest()
        {
            var data = overtimeRequestRepository.GetRegisteredRequest();
            if (data != null)
            {

                return Ok(data);
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data is empty" });
            }
        }
        [HttpPost("InsertCountBonus")]
        public ActionResult InsertCountBonus(FinanceValidation financeValidation)
        {
            var request = overtimeRequestRepository.InsertCountBonus(financeValidation);
            try
            {

                return Ok(request);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
