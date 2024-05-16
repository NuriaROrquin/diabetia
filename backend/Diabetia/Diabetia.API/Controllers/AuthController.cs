using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        private readonly LoginUseCase _loginUseCase;

        public AuthController(ILogger<AuthController> logger, LoginUseCase loginUseCase)
        {
            _logger = logger;
            _loginUseCase = loginUseCase;
        }


        [HttpPost("login")]
        public IActionResult Post([FromBody] LoginRequest request)
        {
            var jwt = _loginUseCase.Login(request.email);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.None,
                Secure = true,
                Path = "/"
            };

            Response.Cookies.Append("jwt", jwt, cookieOptions);

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var res = await _loginUseCase.Register(request.userName, request.email, request.password);

            return Ok(res);
        }
    }
}