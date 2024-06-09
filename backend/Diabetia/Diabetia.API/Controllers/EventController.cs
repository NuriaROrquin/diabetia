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
        private readonly EventGlucoseUseCase _addGlucoseEventUseCase;
        private readonly EventInsulinUseCase _addInsulineEventUseCase;

        public EventController(EventPhysicalActivityUseCase eventPhysicalActivityUseCase, EventGlucoseUseCase addGlucoseEventUseCase, EventInsulinUseCase addInsulineEventUseCase)
        {
            _eventPhysicalActivityUseCase = eventPhysicalActivityUseCase;
            _addGlucoseEventUseCase = addGlucoseEventUseCase;
            _addInsulineEventUseCase = addInsulineEventUseCase;
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
        [Authorize]
        public async Task<IActionResult> AddGlucoseEvent([FromBody] GlucoseEventRequest request)
        {
            await _addGlucoseEventUseCase.AddGlucoseEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Glucose, request.IdDevicePacient, request.IdFoodEvent, request.PostFoodMedition);
            return Ok();
        }

        [HttpPost("AddInsulinEvent")]
        [Authorize]
        public async Task<IActionResult> AddInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _addInsulineEventUseCase.AddInsulinEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Insulin);
            return Ok();
        }
    }
}
