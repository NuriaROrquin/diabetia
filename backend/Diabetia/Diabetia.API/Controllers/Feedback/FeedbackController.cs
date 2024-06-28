using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            return View();
        }
    }
}
