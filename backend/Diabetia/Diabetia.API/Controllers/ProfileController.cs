using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;

        private readonly DataUserUseCase _dataUserUseCase;

        public ProfileController(ILogger<ProfileController> logger, DataUserUseCase dataUserUseCase)
        {
            _logger = logger;
            _dataUserUseCase = dataUserUseCase;
        }

        [HttpGet("getUserInfo")]
        [Authorize]
        public async Task<User> GetEditUserInfo([FromQuery] string email)
        {
            return await _dataUserUseCase.GetEditUserInfo(email);
        }

        [HttpGet("getPatientInfo")]
        [Authorize]
        public async Task<Patient> GetPatientInfo([FromQuery] string email)
        {
            return await _dataUserUseCase.GetPatientInfo(email);
        }

        [HttpGet("getExercisePatientInfo")]
        [Authorize]
        public async Task<Exercise_Patient> GetExerciseInfo([FromQuery] string email)
        {
            return await _dataUserUseCase.GetExerciseInfo(email);
        }

        [HttpGet("getPatientDeviceInfo")]
        [Authorize]
        public async Task<Device_Patient> GetPatientDeviceInfo([FromQuery] string email)
        {
            return await _dataUserUseCase.GetPatientDeviceInfo(email);
        }



    }
}

