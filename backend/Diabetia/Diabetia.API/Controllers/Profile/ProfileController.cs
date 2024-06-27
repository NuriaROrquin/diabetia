using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Diabetia.API.Controllers.Profile
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataUserUseCase _dataUserUseCase;

        public ProfileController(ILogger<ProfileController> logger, DataUserUseCase dataUserUseCase, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _dataUserUseCase = dataUserUseCase;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("getUserInfo")]
        [Authorize]
        public async Task<User> GetEditUserInfo()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            return await _dataUserUseCase.GetEditUserInfo(email);
        }

        [HttpGet("getPatientInfo")]
        [Authorize]
        public async Task<Patient> GetPatientInfo()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            return await _dataUserUseCase.GetPatientInfo(email);
        }

        [HttpGet("getExercisePatientInfo")]
        [Authorize]
        public async Task<Exercise_Patient> GetExerciseInfo()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            return await _dataUserUseCase.GetExerciseInfo(email);
        }

        [HttpGet("getPatientDeviceInfo")]
        [Authorize]
        public async Task<Device_Patient> GetPatientDeviceInfo()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            return await _dataUserUseCase.GetPatientDeviceInfo(email);
        }



    }
}

