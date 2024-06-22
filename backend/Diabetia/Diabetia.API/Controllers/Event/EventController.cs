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
        private readonly FoodManuallyUseCase _eventFoodManuallyUseCase;
        private readonly EventUseCase _getEventUseCase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EventMedicalExaminationUseCase _eventMedicalExaminationUseCase;

        public EventController(EventFoodUseCase eventFoodManuallyUseCase, EventUseCase eventUseCase, EventMedicalExaminationUseCase eventMedicalExaminationUseCase, IHttpContextAccessor httpContextAccessor)
        {
            _eventFoodManuallyUseCase = eventFoodManuallyUseCase;
            _getEventUseCase = eventUseCase;
            _httpContextAccessor = httpContextAccessor;
            _eventMedicalExaminationUseCase = eventMedicalExaminationUseCase;
        }

        //// -------------------------------------------- ⬇️⬇ Food Manually ⬇️⬇ --------------------------------------------------
        //[HttpPost("AddFoodManuallyEvent")]
        //public async Task<EventFoodResponse> AddFoodManuallyEvent([FromBody] EventFoodRequest request)
        //{
        //    EventFoodResponse response = new EventFoodResponse();
        //   var totalChConsumed = await _eventFoodManuallyUseCase.AddFoodManuallyEvent(request.Email, request.EventDate, request.IdKindEvent.Value, request.Ingredients, request.FreeNote);



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
