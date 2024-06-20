using Diabetia.API.DTO;
using Diabetia.Application.UseCases;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly EventInsulinUseCase _eventInsulintUseCase;
        private readonly EventFoodUseCase _eventFoodManuallyUseCase;
        private readonly EventUseCase _getEventUseCase;
        private readonly DataUserUseCase _dataUserUseCase;
        private readonly EventMedicalExaminationUseCase _eventMedicalExaminationUseCase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventController(EventInsulinUseCase eventInsulinUseCase, EventFoodUseCase eventFoodManuallyUseCase, EventUseCase eventUseCase, DataUserUseCase dataUserUseCase, EventMedicalExaminationUseCase eventMedicalExaminationUseCase, IHttpContextAccessor httpContextAccessor)
        {
            //_eventGlucosetUseCase = eventGlucoseUseCase;
            _eventInsulintUseCase = eventInsulinUseCase;
            _eventFoodManuallyUseCase = eventFoodManuallyUseCase;
            _getEventUseCase = eventUseCase;
            _dataUserUseCase = dataUserUseCase;
            _eventMedicalExaminationUseCase = eventMedicalExaminationUseCase;
            _httpContextAccessor = httpContextAccessor;
        }

        //// -------------------------------------------- ⬇️⬇ Insuline ⬇️⬇ --------------------------------------------------
        //[HttpPost("AddInsulinEvent")]
        //public async Task<IActionResult> AddInsulinEvent([FromBody] EventInsulinRequest request)
        //{
        //    await _eventInsulintUseCase.AddInsulinEvent(request.Email, request.IdKindEvent.Value, request.EventDate, request.FreeNote, request.Insulin.Value);
        //    return Ok();
        //}

        //[HttpPost("EditInsulinEvent")]
        //public async Task<IActionResult> EditInsulinEvent([FromBody] EventInsulinRequest request)
        //{
        //    await _eventInsulintUseCase.EditInsulinEvent(request.IdEvent.Value, request.Email, request.EventDate, request.FreeNote, request.Insulin.Value);
        //    return Ok("Evento modificado correctamente");
        //}


        //// -------------------------------------------- ⬇️⬇ Food Manually ⬇️⬇ --------------------------------------------------
        //[HttpPost("AddFoodManuallyEvent")]
        //public async Task<EventFoodResponse> AddFoodManuallyEvent([FromBody] EventFoodRequest request)
        //{
        //    EventFoodResponse response = new EventFoodResponse();
        //   var totalChConsumed = await _eventFoodManuallyUseCase.AddFoodManuallyEvent(request.Email, request.EventDate, request.IdKindEvent.Value, request.Ingredients, request.FreeNote);

        //    var userPatientInfo = await _dataUserUseCase.GetPatientInfo(request.Email);
        //    if (userPatientInfo.ChCorrection != null)
        //    {
        //        var insulinToCorrect = totalChConsumed / userPatientInfo.ChCorrection;
        //        response.InsulinToCorrect = (float)insulinToCorrect;
        //    }

        //    response.ChConsumed = (int)totalChConsumed;

        //    return response;
        //}

        //[HttpPost("EditFoodManuallyEvent")]
        //public async Task<IActionResult> EditFoodManuallyEvent([FromBody] EventFoodRequest request)
        //{
        //    await _eventFoodManuallyUseCase.EditFoodManuallyEvent(request.IdEvent.Value, request.Email, request.EventDate, request.IdKindEvent.Value, request.Ingredients, request.FreeNote);
        //    return Ok();
        //}


        //// -------------------------------------------- ⬇️⬇ Medical Examination ⬇️⬇ --------------------------------------------------
        //[HttpPost("AddMedicalExaminationEvent")]
        //public async Task<IActionResult> AddMedicalExaminationEvent([FromBody] EventMedicalExaminationRequest request)
        //{
        //    await _eventMedicalExaminationUseCase.AddMedicalExaminationEvent(request.Email, request.EventDate, request.File, request.ExaminationType, request.IdProfessional, request.FreeNote);
        //    return Ok();
        //}

        // ------------------------------------ General Actions ------------------------------------------
        [HttpGet("GetEventType/{id}")]
        public async Task<IActionResult> GetEventType([FromRoute] int id)
        {
            var idEvent = id;
            var eventType = await _getEventUseCase.GetEvent(id);
            if (eventType == null)
            {
                return NotFound();
            }
            return Ok(eventType);
        }

        [HttpPost("DeleteEvent/{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            await _getEventUseCase.DeleteEvent(id, email);
            return Ok();
        }
        [HttpGet("GetIngredients")]
        public async Task<IngredientResponse> GetIngredients()
        {
            var ingredients = await _eventFoodManuallyUseCase.GetIngredients();

            var ingredientsMapped = new IngredientResponse
            {
                Ingredients = ingredients,
            };

            return ingredientsMapped;
        }
    }
}
