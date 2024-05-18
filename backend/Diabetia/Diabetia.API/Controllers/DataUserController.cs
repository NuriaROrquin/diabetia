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
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        private readonly DataUserUseCase _dataUserUseCase;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }


        [HttpPost("firstStep")]
        public async Task<IActionResult> Post([FromBody] DataRequest request)
        {
            var res = await _dataUserUseCase.firstStep(request.name, request.email, request.gender, request.lastname, request.weight, request.phone);

            return Ok(res);
        }
    }
}

