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
        public async Task<User> GetUserInfo([FromQuery] string email)
        {
            return await _dataUserUseCase.GetUserInfo(email);
        }


    }
}

