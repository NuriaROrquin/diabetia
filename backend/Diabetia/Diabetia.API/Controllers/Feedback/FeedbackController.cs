using Diabetia.API.DTO.FeedbackRequest;
using Diabetia.API.Mappers;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Feedback
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackUseCase _feedbackUseCase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedbackController(FeedbackUseCase feedbackUseCase, IHttpContextAccessor httpContextAccessor)
        {
            _feedbackUseCase = feedbackUseCase;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAllEventToFeedback")]
        public async Task<IActionResult> GetFoodSummaryEventFeedback()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var events = await _feedbackUseCase.GetEventsToFeedback(email);

            var mappedEvents = events.Select(e => FeedbackMapper.MapToDTO(e)).ToList();

            return Ok(mappedEvents);
        }

        [HttpPost("AddFeedback")]
        public async Task<IActionResult> AddFeedback([FromBody] AddFeedbackRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var feedback = request.toDomain();
            await _feedbackUseCase.AddFeedbackAsync(email, feedback);
            return Ok("Feedback cargado correctamente");
        }
    }
}
