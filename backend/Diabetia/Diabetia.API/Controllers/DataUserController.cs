using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
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
            await _dataUserUseCase.FirstStep(request.Name, request.Email, request.Gender, request.Lastname, request.Weight, request.Phone, request.Birthdate);

            return Ok();
        }

        [HttpPut("secondStep")]
        public async Task<IActionResult> PatientInformationSecondStep([FromBody] PatientRequest request)
        {
            await _dataUserUseCase.SecondStep(request.TypeDiabetes, request.UseInsuline, request.TypeInsuline, request.Email, request.NeedsReminder, request.Frequency, request.HourReminder);

            return Ok();
        }

        [HttpPut("thirdStep")]
        public async Task<IActionResult> PhysicalInformationThirdStep([FromBody] PhysicalRequest request)
        {
            await _dataUserUseCase.ThirdStep(request.Email, request.HaceActividadFisica, request.Frecuencia, request.IdActividadFisica, request.Duracion);

            return Ok();
        }

        [HttpPut("fourthStep")]
        public async Task<IActionResult> DevicesInformationFourthStep([FromBody] DevicesRequest request)
        {
            await _dataUserUseCase.FourthStep(request.Email, request.TieneDispositivo, request.IdDispositivo, request.Frecuencia);

            return Ok();
        }

    }
}

