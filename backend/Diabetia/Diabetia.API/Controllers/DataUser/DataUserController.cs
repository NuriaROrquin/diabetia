using Diabetia.API.DTO.AuthRequest;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Utilities;
using Diabetia.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Diabetia.API.DTO.DataUserRequest;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Diabetia.API.Controllers.DataUser
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;// queda?
        private readonly IJwtTokenService _jwtTokenService; //queda?

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataUserUseCase _dataUserUseCase;
        

        public DataController(ILogger<DataController> logger, DataUserUseCase dataUserUseCase, IJwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger; // queda?
            _jwtTokenService = jwtTokenService; // queda?

            _dataUserUseCase = dataUserUseCase;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPut("firstStep")]
        public async Task<IActionResult> UserInformationFirstStep([FromBody] DataRequest request)
        {

            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = request.ToDomain();
            var patient = await _dataUserUseCase.FirstStep(email, user);
            
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

