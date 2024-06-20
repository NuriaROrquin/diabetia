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
        private readonly IJwtTokenService _jwtTokenService; 

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
            var patient = request.ToDomain();
            var patient_local = await _dataUserUseCase.FirstStep(email, patient);
            
            AuthLoginResponse res = new AuthLoginResponse();
            res.Token = _jwtTokenService.GenerateToken(patient_local.IdUsuario.ToString(), patient_local.IdUsuarioNavigation.Username, request.Email, (int)StepCompletedEnum.STEP1, patient_local.Id);
            return Ok(res);
        }

        [HttpPut("secondStep")]
        public async Task<IActionResult> PatientInformationSecondStep([FromBody] PatientRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var patient = request.ToDomain();
            await _dataUserUseCase.SecondStep(email, patient);
            return Ok();
        }

        [HttpPut("thirdStep")]
        public async Task<IActionResult> PhysicalInformationThirdStep([FromBody] PhysicalRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var patient_actifisica = request.ToDomain();
            await _dataUserUseCase.ThirdStep(email, patient_actifisica);
            return Ok();
        }

        [HttpPut("fourthStep")]
        public async Task<IActionResult> DevicesInformationFourthStep([FromBody] DevicesRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var patient_dispo = request.ToDomain();
            await _dataUserUseCase.FourthStep(email, patient_dispo, request.TieneDispositivo);
            return Ok();
        }

    }
}

