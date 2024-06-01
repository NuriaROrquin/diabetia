using Diabetia.API.DTO;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GlucoseEventController : ControllerBase
    {
        private readonly AddGlucoseEventUseCase _addGlucoseEventUseCase;

        public GlucoseEventController(AddGlucoseEventUseCase addGlucoseEventUseCase)
        {
            _addGlucoseEventUseCase = addGlucoseEventUseCase;
        }

        [HttpPost("AddGlucoseEvent")]
        public async Task <IActionResult> AddGlucoseEvent([FromBody] GlucoseEventRequest request)
        {
            await _addGlucoseEventUseCase.AddGlucoseEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Glucose, request.IdDevicePacient, request.IdFoodEvent, request.PostFoodMedition);
            return Ok();
        }
    }
}
