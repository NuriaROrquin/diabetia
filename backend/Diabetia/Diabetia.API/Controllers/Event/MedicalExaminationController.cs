using Diabetia.API.DTO.EventRequest.MedicalExamination;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Event
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MedicalExaminationController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MedicalExaminationUseCase _medicalExaminationUseCase;

        public MedicalExaminationController(IHttpContextAccessor httpContextAccessor, MedicalExaminationUseCase medicalExaminationUseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _medicalExaminationUseCase = medicalExaminationUseCase;
        }

        [HttpPost("AddMedicalExaminationEvent")]
        public async Task<IActionResult> AddFreeNoteEvent([FromBody] AddMedicalExaminationRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var medicalExamination = request.ToDomain();
            await _medicalExaminationUseCase.AddMedicalExaminationEventAsync(email,medicalExamination);
            return Ok("El estudio médico fue cargado exitosamente");
        }
    }
}
