using Diabetia.API.DTO.EventRequest.PhysicalActivity;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Event
{
    [ApiController]
    [Route("events/[controller]")]
    [Authorize]
    public class PhysicalActivityController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PhysicalActivityUseCase _eventPhysicalActivityUseCase;
        public PhysicalActivityController(IHttpContextAccessor httpContextAccessor, PhysicalActivityUseCase eventPhysicalActivityUseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _eventPhysicalActivityUseCase = eventPhysicalActivityUseCase;
        }

        [HttpPost] // VER LOS PROTOCOLOS
        public async Task<IActionResult> AddPhysicalEvent([FromBody] AddPhysicalRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var physicalEvent = request.ToDomain();
            await _eventPhysicalActivityUseCase.AddPhysicalEventAsync(email, physicalEvent);
            return Ok("Evento creado correctamente");
        }

        [HttpPut] // VER LOS PROTOCOLOS
        public async Task<IActionResult> EditPhysicalEvent([FromBody] EditPhysicalRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var physicalEvent = request.ToDomain();
            await _eventPhysicalActivityUseCase.EditPhysicalEventAsync(email, physicalEvent);
            return Ok("Evento modificado correctamente");
        }
    }
}
