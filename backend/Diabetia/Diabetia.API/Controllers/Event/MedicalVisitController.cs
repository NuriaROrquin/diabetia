using Diabetia.API.DTO.EventRequest.MedicalVisit;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Event
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MedicalVisitController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MedicalVisitUseCase _eventMedicalVisitUseCase;
        public MedicalVisitController(IHttpContextAccessor httpContextAccessor, MedicalVisitUseCase eventMedicalVisitUseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _eventMedicalVisitUseCase = eventMedicalVisitUseCase;
        }

        [HttpPost("AddMedicalVisitEvent")] // VER PROTOCOLOS
        public async Task<IActionResult> AddMedicalEventAsync([FromBody] AddMedicalVisitRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var medicalVisit = request.ToDomain();
            await _eventMedicalVisitUseCase.AddMedicalVisitEventAsync(email, medicalVisit); // TODO: Recordatorio
            return Ok("Visita médica agregada correctamente");
        }

        [HttpPost("EditMedicalVisitEvent")] // VER PROTOCOLOS
        public async Task<IActionResult> EditMedicalEventAsync([FromBody] EditMedicalVisitRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst("email")?.Value;
            await _eventMedicalVisitUseCase.EditMedicalVisitEventAsync(email, request.ToDomain(request)); // TODO: Recordatorio
            return Ok("Visita médica modificada correctamente");
        }
    }
}
