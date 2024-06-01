using Diabetia.API.DTO;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly AddPhysicalEventUseCase _addPhysicalEventUseCase;

        public EventController(AddPhysicalEventUseCase addPhysicalEventUseCase)
        {
            _addPhysicalEventUseCase = addPhysicalEventUseCase;
        }

        [HttpPost("AddPhysicalEvent")]
        public async Task <IActionResult> AddPhysicalEvent([FromBody] EventRequest request)
        {
            await _addPhysicalEventUseCase.AddPhysicalEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.PhysicalActivity, request.IniciateTime, request.FinishTime);
            return Ok();
        }
    }
}
