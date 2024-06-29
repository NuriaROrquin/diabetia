using Diabetia.API.DTO.FeedbackRequest;
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

        [HttpPost("AddFeedback")]
        public async Task<IActionResult> AddFeedbackAsync([FromBody] AddFeedbackRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var feedback = request.ToDomain();
            await _feedbackUseCase.AddFeedbackToEventAsync(email, feedback);
            return Ok("Se agregó el Feedback correctamente");
        }
    }
}
