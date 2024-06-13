using Diabetia.API.DTO.AuthRequest;
using Diabetia.Application.UseCases;
using Diabetia.Common.Utilities;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Diabetia.API.Controllers.DataUser
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        private readonly DataUserUseCase _dataUserUseCase;
        private readonly IJwtTokenService _jwtTokenService;

        public DataController(ILogger<DataController> logger, DataUserUseCase dataUserUseCase, IJwtTokenService jwtTokenService)
        {
            _logger = logger;
            _dataUserUseCase = dataUserUseCase;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPut("firstStep")]
        public async Task<IActionResult> UserInformationFirstStep([FromBody] DataRequest request)
        {
            var patient = await _dataUserUseCase.FirstStep(request.Name, request.Email, request.Gender, request.Lastname, request.Weight, request.Phone, request.Birthdate);

            AuthLoginResponse res = new AuthLoginResponse();

            res.Token = _jwtTokenService.GenerateToken(patient.IdUsuario.ToString(), patient.IdUsuarioNavigation.Username, request.Email, (int)StepCompletedEnum.STEP1, patient.Id);

            return Ok(res);
        }

        [HttpPut("secondStep")]
        public async Task<IActionResult> PatientInformationSecondStep([FromBody] PatientRequest request)
        {
            await _dataUserUseCase.SecondStep(request.TypeDiabetes, request.UseInsuline, request.TypeInsuline, request.Email, request.NeedsReminder, request.Frequency, request.HourReminder, request.InsulinePerCH);

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

