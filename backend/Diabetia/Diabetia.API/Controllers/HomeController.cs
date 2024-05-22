using Diabetia.Application.UseCases;
using Diabetia.Domain.Services;
using Microsoft.AspNetCore.Mvc;
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



        [HttpPost("physicalActivity")]
        public async Task<IActionResult> ShowPhysicalMetrics([FromBody] MetricsRequest request)
        {
            await _homeUseCase.PhysicalActivity(request.IdUser, request.IdEvento );

            return Ok();
        }

    }
}

