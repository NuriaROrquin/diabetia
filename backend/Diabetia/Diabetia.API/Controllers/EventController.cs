using Diabetia.API.DTO;
using Diabetia.API.DTO.EventRequest;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventPhysicalActivityUseCase _eventPhysicalActivityUseCase;
        private readonly EventGlucoseUseCase _eventGlucosetUseCase;
        private readonly EventInsulinUseCase _eventInsulintUseCase;
        private readonly EventFoodUseCase _eventFoodManuallyUseCase;
        private readonly EventUseCase _getEventUseCase;
        private readonly DataUserUseCase _dataUserUseCase;
                       

        public EventController(EventPhysicalActivityUseCase eventPhysicalActivityUseCase, EventGlucoseUseCase evemtGlucoseUseCase, EventInsulinUseCase eventInsulinUseCase, EventFoodUseCase eventFoodManuallyUseCase, EventUseCase eventUseCase, DataUserUseCase dataUserUseCase)
        {
            _eventPhysicalActivityUseCase = eventPhysicalActivityUseCase;
            _eventGlucosetUseCase = evemtGlucoseUseCase;
            _eventInsulintUseCase = eventInsulinUseCase;
            _eventFoodManuallyUseCase = eventFoodManuallyUseCase;
            _getEventUseCase = eventUseCase;
            _dataUserUseCase = dataUserUseCase;
        }

        [HttpPost("AddPhysicalEvent")]
        [Authorize]
        public async Task <IActionResult> AddPhysicalEvent([FromBody] EventAddPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.AddPhysicalEventAsync(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.PhysicalActivity, request.IniciateTime, request.FinishTime);
            return Ok("Evento creado correctamente");
        }

        [HttpPost("EditPhysicalEvent")]
        [Authorize]
        public async Task<IActionResult> EditPhysicalEvent([FromBody] EventEditPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.EditPhysicalEventAsync(request.Email, request.EventId, request.EventDate, request.PhysicalActivity, request.IniciateTime, request.FinishTime, request.FreeNote);
            return Ok("Evento modificado correctamente"); ;
        }


        [HttpPost("AddGlucoseEvent")]
        public async Task<IActionResult> AddGlucoseEvent([FromBody] EventGlucoseRequest request)
        {
            await _eventGlucosetUseCase.AddGlucoseEvent(request.Email, request.IdKindEvent.Value, request.EventDate, request.FreeNote, request.Glucose.Value, request.IdFoodEvent, request.PostFoodMedition);
            return Ok();
        }

        [HttpPost("EditGlucoseEvent")]
        public async Task<IActionResult> EditGlucoseEvent([FromBody] EventGlucoseRequest request)
        {
            await _eventGlucosetUseCase.EditGlucoseEvent(request.IdEvent.Value, request.Email, request.EventDate, request.FreeNote, request.Glucose.Value, request.IdFoodEvent, request.PostFoodMedition);
            return Ok("Evento modificado correctamente");
        }


        [HttpPost("AddInsulinEvent")]
        public async Task<IActionResult> AddInsulinEvent([FromBody] EventInsulinRequest request)
        {
            await _eventInsulintUseCase.AddInsulinEvent(request.Email, request.IdKindEvent.Value, request.EventDate, request.FreeNote, request.Insulin.Value);
            return Ok();
        }

        [HttpPost("EditInsulinEvent")]
        public async Task<IActionResult> EditInsulinEvent([FromBody] EventInsulinRequest request)
        {
            await _eventInsulintUseCase.EditInsulinEvent(request.IdEvent.Value, request.Email, request.EventDate, request.FreeNote, request.Insulin.Value);
            return Ok("Evento modificado correctamente");
        }

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
            await _getEventUseCase.DeleteEvent(id);
            return Ok();
        }

        [HttpPost("AddFoodManuallyEvent")]
        public async Task<EventFoodResponse> AddFoodManuallyEvent([FromBody] EventFoodRequest request)
        {
            EventFoodResponse response = new EventFoodResponse();
           var totalChConsumed = await _eventFoodManuallyUseCase.AddFoodManuallyEvent(request.Email, request.EventDate, request.IdKindEvent.Value, request.Ingredients, request.FreeNote);

            var userPatientInfo = await _dataUserUseCase.GetPatientInfo(request.Email);

            var insulinToCorrect = totalChConsumed / userPatientInfo.ChCorrection;

            response.InsulinToCorrect = (float)insulinToCorrect;
            response.ChConsumed = (int)totalChConsumed;

            return response;
        }

        [HttpPost("EditFoodManuallyEvent")]
        public async Task<IActionResult> EditFoodManuallyEvent([FromBody] EventFoodRequest request)
        {
            await _eventFoodManuallyUseCase.EditFoodManuallyEvent(request.IdEvent.Value, request.Email, request.EventDate, request.IdKindEvent.Value, request.Ingredients, request.FreeNote);
            return Ok();
        }
    }
}
