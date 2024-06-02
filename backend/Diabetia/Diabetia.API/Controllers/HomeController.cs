using Diabetia.Application.UseCases;
using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Diabetia.API.DTO;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HomeUseCase _homeUseCase;

        public HomeController(ILogger<HomeController> logger, HomeUseCase homeUseCase)
        {
            _logger = logger;
            _homeUseCase = homeUseCase;
        }


        
        [HttpPost("metrics")]
        public async Task<MetricsResponse> ShowAllMetrics([FromBody] MetricsRequest request)
        {
            Metrics metrics = await _homeUseCase.ShowMetrics(request.Email);

            MetricsResponse metricsResponse = new MetricsResponse
            {
                ChMetrics = metrics.Carbohydrates,
                PhysicalActivity = metrics.PhysicalActivity,
                Glycemia = metrics.Glycemia,
                Hypoglycemia = metrics.Hypoglycemia,
                Hyperglycemia = metrics.Hyperglycemia,
                Insulin = metrics.Insulin,
            };

            return metricsResponse;
        }

    }
}

