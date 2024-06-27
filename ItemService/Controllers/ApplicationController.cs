using ItemService.Dto;
using ItemService.Models;
using ItemService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers
{
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private IApplicationService _applService;

        public ApplicationController(IApplicationService applService)
        {
            _applService = applService;
        }

        [HttpPost]
        [Route("application/create")]
        public async Task<ActionResult<GetApplDto>> CreateApplication(ApplicationCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var ApplicationId = await _applService.CreateApplication(model);
                return GetApplicationInfo(ApplicationId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("application/info/{id}")]
        public ActionResult<GetApplDto> GetApplicationInfo(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _applService.GetApplication(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This application does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("user/{id}/applications")]
        public ActionResult<List<GetApplDto>> GetApplicationsOwnedByUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _applService.GetApplicationsOwnedByUser(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This application does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet]
        [Route("user/{id}/incomingApplications")]
        public ActionResult<List<GetApplDto>> GetIncomingApplications(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return _applService.GetIncomingApplications(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This application does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPut]
        [Route("application/{id}/edit")]
        public async Task<ActionResult<GetApplDto>> UpdateApplication(int id, ApplicationUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _applService.UpdateApplication(id, model);
                return GetApplicationInfo(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "This application does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete]
        [Route("application/{id}/delete")]
        public async Task<ActionResult> DeleteApplication(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _applService.DeleteApplication(id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "This application does not exist")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        [Route("application/reservationTime")]
        public ActionResult<List<ResReservTimeDto>> FindReservationTimeForDateItems(ReqReservTimeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            return _applService.FindTimeForAppl(model);
        }

        [HttpPost]
        [Route("applyItemInApplication")]
        public async Task<IActionResult> ApplyItemInApplication([FromBody] int ItemInApplicationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _applService.ChangeItemInApplState(ApplicationItemStates.Approved, ItemInApplicationId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "This item does not exist in application")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        [Route("declineItemInApplication")]
        public async Task<IActionResult> DeclineItemInApplication([FromBody] int ItemInApplicationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _applService.ChangeItemInApplState(ApplicationItemStates.Declined, ItemInApplicationId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "This item does not exist in application")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpPost]
        [Route("changeItemInApplication/{id}")]
        public async Task<IActionResult> ChangeTimesItemInApplication(ResReservTimeDto times, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _applService.ChangeTimesItemInApplState(times, id);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == "This item does not exist in application")
                {
                    return StatusCode(400, ex.Message);
                }
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
