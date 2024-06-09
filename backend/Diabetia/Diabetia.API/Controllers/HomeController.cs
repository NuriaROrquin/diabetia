using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Diabetia.Common.Utilities;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<MetricsResponse> ShowAllMetrics([FromBody] MetricsRequest request)
        {
            Metrics metrics = await _homeUseCase.ShowMetrics(request.Email, request.DateFilter);

            MetricsResponse metricsResponse = new MetricsResponse
            {
                Carbohidrates = new Carbohidrates
                {
                    Quantity = metrics.Carbohydrates
                },
                PhysicalActivity = new PhysicalActivity
                {
                    Quantity = metrics.PhysicalActivity,
                    IsWarning = metrics.PhysicalActivity < 30
                },
                Glycemia = new Glycemia
                {
                    Quantity = metrics.Glycemia,
                    IsWarning = metrics.Glycemia < (int)GlucoseEnum.HIPOGLUCEMIA || metrics.Glycemia > (int)GlucoseEnum.HIPERGLUCEMIA
                },
                Hypoglycemia = new Hypoglycemia
                {
                    Quantity = metrics.Hypoglycemia,
                    IsWarning = metrics.Hypoglycemia >= 1
                },
                Hyperglycemia = new Hyperglycemia
                {
                    Quantity = metrics.Hyperglycemia,
                    IsWarning = metrics.Hyperglycemia >= 1
                },
                Insulin = new Insulin
                {
                    Quantity = metrics.Insulin
                },
            };

            return metricsResponse;
        }

    }
}

