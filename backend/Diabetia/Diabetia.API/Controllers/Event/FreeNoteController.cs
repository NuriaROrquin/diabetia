using Diabetia.API.DTO.EventRequest.FreeNote;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Event
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FreeNoteController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FreeNoteUseCase _eventFreeNoteUseCase;

        public FreeNoteController(IHttpContextAccessor httpContextAccessor, FreeNoteUseCase eventFreeNoteUseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _eventFreeNoteUseCase = eventFreeNoteUseCase;
        }

        [HttpPost("AddFreeNoteEvent")]
        public async Task<IActionResult> AddFreeNoteEvent([FromBody] AddFreeNoteRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var freeNoteEvent = request.ToDomain();
            await _eventFreeNoteUseCase.AddFreeNoteEventAsync(email, freeNoteEvent);
            return Ok("La nota fue cargada exitosamente");
        }

        [HttpPost("EditFreeNoteEvent")]
        public async Task<IActionResult> EditFreeNoteEvent([FromBody] EditFreeNoteRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var freeNoteEvent = request.ToDomain();
            await _eventFreeNoteUseCase.EditFreeNoteEventAsync(email, freeNoteEvent);
            return Ok("La nota fue modificada exitosamente");
        }
    }
}
