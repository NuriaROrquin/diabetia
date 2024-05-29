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

        [HttpPut("firstStep")]
        public async Task<IActionResult> UserInformationFirstStep([FromBody] DataRequest request)
        {
            await _dataUserUseCase.FirstStep(request.name, request.email, request.gender, request.lastname, request.weight, request.phone);

            return Ok();
        }

        [HttpPut("secondStep")]
        public async Task<IActionResult> PatientInformationSecondStep([FromBody] PatientRequest request)
        {
            await _dataUserUseCase.SecondStep(request.typeDiabetes, request.useInsuline, request.typeInsuline, request.email);

            return Ok();
        }

        [HttpPut("thirdStep")]
        public async Task<IActionResult> PhysicalInformationThirdStep([FromBody] PhysicalRequest request)
        {
            await _dataUserUseCase.ThirdStep();

            return Ok();
        }


    }
}

