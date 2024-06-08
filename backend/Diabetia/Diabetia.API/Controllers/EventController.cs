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
        private readonly AddInsulinEventUseCase _addInsulinEventUseCase;


        public EventController(EventPhysicalActivityUseCase eventPhysicalActivityUseCase, AddGlucoseEventUseCase addGlucoseEventUseCase, AddInsulinEventUseCase addInsulinEventUseCase)
        {
            _eventPhysicalActivityUseCase = eventPhysicalActivityUseCase;
            _addGlucoseEventUseCase = addGlucoseEventUseCase;
            _addInsulinEventUseCase = addInsulinEventUseCase;
        }

        [HttpPost("AddPhysicalEvent")]
        public async Task <IActionResult> AddPhysicalEvent([FromBody] EventAddPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.AddPhysicalEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.PhysicalActivity, request.IniciateTime, request.FinishTime);
            return Ok();
        }

        [HttpPost("EditPhysicalEvent")]
        public async Task<IActionResult> EditEventInformation([FromBody] EventEditPhysicalRequest request)
        {
            await _eventPhysicalActivityUseCase.EditPhysicalEvent(request.Email, request.EventId, request.EventDate, request.PhysicalActivity, request.IniciateTime, request.FinishTime, request.FreeNote);
            return Ok("Evento modificado correctamente"); ;
        }


        [HttpPost("AddGlucoseEvent")]
        public async Task<IActionResult> AddGlucoseEvent([FromBody] GlucoseEventRequest request)
        {
            await _addGlucoseEventUseCase.AddGlucoseEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Glucose, request.IdDevicePacient, request.IdFoodEvent, request.PostFoodMedition);
            return Ok();
        }

        [HttpPost("EditGlucoseEvent")]
        public async Task<IActionResult> EditGlucoseEvent([FromBody] GlucoseEventRequest request)
        {
            await _addGlucoseEventUseCase.EditGlucoseEvent(request.IdEvent.Value, request.Email, request.EventDate, request.FreeNote, request.Glucose, request.IdDevicePacient, request.IdFoodEvent, request.PostFoodMedition);
            return Ok();
        }
        /*
        [HttpPost("DeleteGlucoseEvent")]
        public async Task<IActionResult> DeleteInsulinEvent([FromBody] GlucoseEventRequest request)
        {
            await _addGlucoseEventUseCase.DeleteGlucoseEvent(request.IdEvent.Value, request.Email);
            return Ok();
        }*/

        [HttpPost("AddInsulinEvent")]
        public async Task<IActionResult> AddInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _addInsulinEventUseCase.AddInsulinEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Insulin);
            return Ok();
        }

        [HttpPost("EditInsulinEvent")]
        public async Task<IActionResult> EditInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _addInsulinEventUseCase.EditInsulinEvent(request.IdEvent.Value, request.Email, request.EventDate, request.FreeNote, request.Insulin);
            return Ok();
        }

        [HttpPost("DeleteInsulinEvent")]
        public async Task<IActionResult> DeleteInsulinEvent([FromBody] InsulinEventRequest request)
        {
            await _addInsulinEventUseCase.DeleteInsulinEvent(request.IdEvent.Value, request.Email);
            return Ok();
        }
    }
}
