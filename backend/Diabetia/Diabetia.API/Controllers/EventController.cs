using Diabetia.API.DTO;
using Diabetia.API.DTO.EventRequest;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventPhysicalActivityUseCase _eventPhysicalActivityUseCase;
        private readonly AddGlucoseEventUseCase _addGlucoseEventUseCase;
        private readonly AddInsulinEventUseCase _addInsulineEventUseCase;

        public EventController(EventPhysicalActivityUseCase eventPhysicalActivityUseCase, AddGlucoseEventUseCase addGlucoseEventUseCase, AddInsulinEventUseCase addInsulineEventUseCase)
        {
            _eventPhysicalActivityUseCase = eventPhysicalActivityUseCase;
            _addGlucoseEventUseCase = addGlucoseEventUseCase;
            _addInsulineEventUseCase = addInsulineEventUseCase;
        }

        [HttpPost("AddPhysicalEvent")]
        public async Task <IActionResult> AddPhysicalEvent([FromBody] EventAddPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.AddPhysicalEventAsync(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.PhysicalActivity, request.IniciateTime, request.FinishTime);
            return Ok();
        }

        [HttpPost("EditPhysicalEvent")]
        public async Task<IActionResult> EditPhysicalEvent([FromBody] EventEditPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.EditPhysicalEventAsync(request.Email, request.EventId, request.EventDate, request.PhysicalActivity, request.IniciateTime, request.FinishTime, request.FreeNote);
            return Ok("Evento modificado correctamente"); ;
        }

        [HttpPost("DeletePhysicalEvent")]
        public async Task<IActionResult> DeletePhysicalEvent([FromBody] EventDeletePhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.DeletePhysicalEventAsync(request.Email, request.EventId);
            return Ok("Evento eliminado correctamente"); ;
        }


        [HttpPost("AddGlucoseEvent")]
        public async Task<IActionResult> AddGlucoseEvent([FromBody] GlucoseEventRequest request)
        {
            await _addGlucoseEventUseCase.AddGlucoseEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Glucose, request.IdDevicePacient, request.IdFoodEvent, request.PostFoodMedition);
            return Ok();
        }

        [HttpPost("AddInsulinEvent")]
        public async Task<IActionResult> AddInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _addInsulineEventUseCase.AddInsulinEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Insulin);
            return Ok();
        }
    }
}
