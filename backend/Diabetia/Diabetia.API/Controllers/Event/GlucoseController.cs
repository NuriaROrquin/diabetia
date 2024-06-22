using Diabetia.API.DTO.EventRequest.Glucose;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Diabetia.API.Controllers.Event
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GlucoseController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GlucoseUseCase _eventGlucoseUseCase;
        public GlucoseController(IHttpContextAccessor httpContextAccessor, GlucoseUseCase eventGlucoseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _eventGlucoseUseCase = eventGlucoseCase;
        }

        [HttpPost("AddGlucoseEvent")] 
        public async Task<IActionResult> AddGlucoseAsync([FromBody] AddGlucoseRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var glucose = request.ToDomain();
            await _eventGlucoseUseCase.AddGlucoseEventAsync(email, glucose);
            return Ok("Registro de glucosa agregado correctamente");
        }
        
        [HttpPost("EditGlucoseEvent")]
        public async Task<IActionResult> EditGlucoseEventAsync([FromBody] EditGlucoseRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var glucose = request.ToDomain();
            await _eventGlucoseUseCase.EditGlucoseEventAsync(email, glucose);
            return Ok("Registro de glucosa modificado correctamente");
        }
        
    }
}
