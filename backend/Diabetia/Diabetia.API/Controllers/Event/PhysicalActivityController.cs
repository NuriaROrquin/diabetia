using Diabetia.API.DTO.EventRequest.PhysicalActivity;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diabetia.API.Controllers.Event
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpPost("AddPhysicalEvent")] // VER LOS PROTOCOLOS
        public async Task<IActionResult> AddPhysicalEvent([FromBody] AddPhysicalRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst("email")?.Value;
            await _eventPhysicalActivityUseCase.AddPhysicalEventAsync(email, request.ToDomain(request));
            return Ok("Evento creado correctamente");
        }

        [HttpPost("EditPhysicalEvent")] // VER LOS PROTOCOLOS
        public async Task<IActionResult> EditPhysicalEvent([FromBody] EditPhysicalRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst("email")?.Value;
            await _eventPhysicalActivityUseCase.EditPhysicalEventAsync(email, request.ToDomain(request));
            return Ok("Evento modificado correctamente"); ;
        }
    }
}
