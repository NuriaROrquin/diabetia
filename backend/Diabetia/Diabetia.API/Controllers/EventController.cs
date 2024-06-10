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
        private readonly EventFoodManuallyUseCase _eventFoodManuallyUseCase;
        private readonly EventUseCase _getEventUseCase;
                       

        public EventController(EventPhysicalActivityUseCase eventPhysicalActivityUseCase, EventGlucoseUseCase evemtGlucoseUseCase, EventInsulinUseCase eventInsulinUseCase, EventFoodManuallyUseCase eventFoodManuallyUseCase, EventUseCase eventUseCase)
        {
            _eventPhysicalActivityUseCase = eventPhysicalActivityUseCase;
            _eventGlucosetUseCase = evemtGlucoseUseCase;
            _eventInsulintUseCase = eventInsulinUseCase;
            _eventFoodManuallyUseCase = eventFoodManuallyUseCase;
            _getEventUseCase = eventUseCase;
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

        [HttpPost("DeletePhysicalEvent")]
        [Authorize]
        public async Task<IActionResult> DeletePhysicalEvent([FromBody] EventDeletePhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.DeletePhysicalEventAsync(request.Email, request.EventId);
            return Ok("Evento eliminado correctamente"); ;
        }

        [HttpPost("AddGlucoseEvent")]
        public async Task<IActionResult> AddGlucoseEvent([FromBody] GlucoseEventRequest request)
        {
            await _eventGlucosetUseCase.AddGlucoseEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Glucose.Value, request.IdDevicePacient, request.IdFoodEvent, request.PostFoodMedition);
            return Ok();
        }

        [HttpPost("EditGlucoseEvent")]
        public async Task<IActionResult> EditGlucoseEvent([FromBody] GlucoseEventRequest request)
        {
            await _eventGlucosetUseCase.EditGlucoseEvent(request.IdEvent.Value, request.Email, request.EventDate, request.FreeNote, request.Glucose.Value, request.IdDevicePacient, request.IdFoodEvent, request.PostFoodMedition);
            return Ok("Evento modificado correctamente");
        }

        [HttpPost("DeleteGlucoseEvent")]
        public async Task<IActionResult> DeleteInsulinEvent([FromBody] GlucoseEventRequest request)
        {
            await _eventGlucosetUseCase.DeleteGlucoseEvent(request.IdEvent.Value, request.Email);
            return Ok("Evento eliminado correctamente");
        }

        [HttpPost("AddInsulinEvent")]
        public async Task<IActionResult> AddInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _eventInsulintUseCase.AddInsulinEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Insulin.Value);
            return Ok();
        }

        [HttpPost("EditInsulinEvent")]
        public async Task<IActionResult> EditInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _eventInsulintUseCase.EditInsulinEvent(request.IdEvent.Value, request.Email, request.EventDate, request.FreeNote, request.Insulin.Value);
            return Ok("Evento modificado correctamente");
        }

       /* [HttpPost("DeleteInsulinEvent")]
        public async Task<IActionResult> DeleteInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _eventInsulintUseCase.DeleteInsulinEvent(request.IdEvent.Value);
            return Ok();
        } */

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

        [HttpGet("DeleteEvent/{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            await _getEventUseCase.DeleteEvent(id);
            return Ok();
        }

        [HttpPost("AddFoodManuallyEvent")]
        public async Task<IActionResult> AddFoodManuallyEvent([FromBody] FoodManuallyRequest request)
        {
            await _eventFoodManuallyUseCase.AddFoodManuallyEvent(request.Email, request.EventDate, request.IdKindEvent, request.Ingredients, request.FreeNote);
            return Ok();
        }

        [HttpPost("EditFoodManuallyEvent")]
        public async Task<IActionResult> EditFoodManuallyEvent([FromBody] FoodManuallyRequest request)
        {
            await _eventFoodManuallyUseCase.EditFoodManuallyEvent(request.IdEvent.Value, request.Email, request.EventDate, request.IdKindEvent, request.Ingredients, request.FreeNote);
            return Ok();
        }
    }
}
