using Diabetia.API.DTO.EventRequest;
using Diabetia.API.DTO.EventRequest.MedicalVisit;
using Diabetia.API.DTO.EventRequest.PhysicalActivity;
using Diabetia.Application.UseCases.EventUseCases;
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
        private readonly EventUseCase _getEventUseCase;
        private readonly EventMedicalVisitUseCase _eventMedicalVisitUseCase;

        public EventController(EventPhysicalActivityUseCase eventPhysicalActivityUseCase, EventGlucoseUseCase evemtGlucoseUseCase, EventInsulinUseCase eventInsulinUseCase, EventUseCase eventUseCase, EventMedicalVisitUseCase eventMedicalVisitUseCase)
        {
            _eventPhysicalActivityUseCase = eventPhysicalActivityUseCase;
            _eventGlucosetUseCase = evemtGlucoseUseCase;
            _eventInsulintUseCase = eventInsulinUseCase;
            _getEventUseCase = eventUseCase;
            _eventMedicalVisitUseCase = eventMedicalVisitUseCase;
        }

        // ------------------------------------------- Physical Event -------------------------------------------
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddPhysicalEvent")]
        [Authorize]
        public async Task <IActionResult> AddPhysicalEvent([FromBody] EventAddPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.AddPhysicalEventAsync(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.PhysicalActivity, request.IniciateTime, request.FinishTime);
            return Ok("Evento creado correctamente");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("EditPhysicalEvent")]
        [Authorize]
        public async Task<IActionResult> EditPhysicalEvent([FromBody] EventEditPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.EditPhysicalEventAsync(request.Email, request.EventId, request.EventDate, request.PhysicalActivity, request.IniciateTime, request.FinishTime, request.FreeNote);
            return Ok("Evento modificado correctamente"); ;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("DeletePhysicalEvent")]
        [Authorize]
        public async Task<IActionResult> DeletePhysicalEvent([FromBody] EventDeletePhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.DeletePhysicalEventAsync(request.Email, request.EventId);
            return Ok("Evento eliminado correctamente"); ;
        }

        // ------------------------------------------- Glucose Event -------------------------------------------
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

        // ------------------------------------------- Insuline Event -------------------------------------------
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

        [HttpPost("DeleteInsulinEvent")]
        public async Task<IActionResult> DeleteInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _eventInsulintUseCase.DeleteInsulinEvent(request.IdEvent.Value);
            return Ok();
        }

        // ------------------------------------------- Medical Visit Event -------------------------------------------
        [HttpPost("AddMedicalVisitEvent")]
        public async Task<IActionResult> AddMedicalEventAsync([FromBody] EventAddMedicalVisitRequest request)
        {
            await _eventMedicalVisitUseCase.AddMedicalVisitEventAsync(request.Email, request.KindEventId, request.EventDate, request.ProfessionalId, request.Recordatory, request.RecordatoryDate, request.Description);
            return Ok("Visita médica agregada correctamente");
        }

        [HttpGet("GetEventType/{id}")]
        public async Task<IActionResult> GetEventType([FromRoute]int id)
        {
            var idEvent = id;
            var eventType = await _getEventUseCase.GetEvent(id);
            if (eventType == null)
            {
                return NotFound();
            }
            return Ok(eventType);
        }
    }
}
