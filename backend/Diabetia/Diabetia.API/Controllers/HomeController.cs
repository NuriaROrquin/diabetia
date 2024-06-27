using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Diabetia.Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Diabetia.API.DTO.HomeRequest;
using System.Security.Claims;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HomeUseCase _homeUseCase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, HomeUseCase homeUseCase, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _homeUseCase = homeUseCase;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("metrics")]
        public async Task<MetricsResponse> ShowAllMetrics([FromQuery] DateFilter? dateFilter)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            Metrics metrics = await _homeUseCase.ShowMetrics(email, dateFilter);

            MetricsResponse metricsResponse = new MetricsResponse(metrics);

            return metricsResponse;
        }

        [HttpGet("timeline")]
        public async Task<TimelineResponse> GetTimeline() 
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            Timeline timeline = await _homeUseCase.GetTimeline(email);

            TimelineResponse timelineResponse = new TimelineResponse(timeline);

            return timelineResponse;
        }
    }
}

