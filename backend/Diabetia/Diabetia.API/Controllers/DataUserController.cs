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

        public DataController(ILogger<DataController> logger, DataUserUseCase dataUserUseCase)
        {
            _logger = logger;
            _dataUserUseCase = dataUserUseCase;
        }


        [HttpPost("firstStep")]
        public async Task<IActionResult> PostFirstStep([FromBody] DataRequest request)
        {
            await _dataUserUseCase.FirstStep(request.name, request.email, request.gender, request.lastname, request.weight, request.phone);

            return Ok();
        }

        [HttpPost("secondStep")]
        public async Task<IActionResult> PostSecondStep([FromBody] PatientRequest request)
        {
            await _dataUserUseCase.SecondStep(request.typeDiabetes, request.useInsuline, request.typeInsuline, request.email);

            return Ok();
        }
    }
}

