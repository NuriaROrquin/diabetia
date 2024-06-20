using Diabetia.API.DTO.EventRequest.Glucose;
using Diabetia.API.DTO.EventRequest.Insuline;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Event
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class InsulinController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly InsulinUseCase _insulinUseCase;
        public InsulinController(IHttpContextAccessor httpContextAccessor, InsulinUseCase insulinUseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _insulinUseCase = insulinUseCase;
        }

        [HttpPost("AddInsulinEvent")]
        public async Task<IActionResult> AddInsulinAsync([FromBody] AddInsulinRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var insulin = request.ToDomain();
            await _insulinUseCase.AddInsulinEventAsync(email, insulin);
            return Ok("Registro de insulina agregado correctamente");
        }
        
        [HttpPost("EditInsulinEvent")]
        public async Task<IActionResult> EditMedicalEventAsync([FromBody] EditInsulinRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var insulin = request.ToDomain();
            await _insulinUseCase.EditInsulinEventAsync(email, insulin);
            return Ok("Registro de insulina modificado correctamente");
        }

    }
}
