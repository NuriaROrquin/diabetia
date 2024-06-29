using Diabetia.API.DTO.FeedbackResponse;
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

        [HttpGet("GetFoodSummaryEventFeedback")]
        public async Task<IActionResult> ShowFoodEventWithoutFeedback()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var @events = await _feedbackUseCase.GetFoodToFeedback(email);
            var foodResponse = events.Select(e => FoodEventResponse.FromObject(e)).ToList();

            return Ok(foodResponse);
        }

        [HttpGet("GetPhysicalActivitySummaryEventFeedback")]
        public async Task<IActionResult> ShowPhysicalActivityEventWithoutFeedback()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var @events = await _feedbackUseCase.GetPhysicalActivityToFeedback(email);
            var activityResponse = events.Select(e => PhysicalActivityResponse.FromObject(e)).ToList();

            return Ok(activityResponse);
        }
    }
}
