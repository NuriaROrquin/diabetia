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
        private readonly RegisterUseCase _registerUseCase;
        private readonly ConfirmUserEmailUseCase _confirmUserEmailUseCase;
        private readonly ForgotPasswordUseCase _forgotPasswordUseCase;
        private readonly ConfirmForgotPasswordCodeUseCase _confirmForgotPasswordCodeUseCase;

        public AuthController(ILogger<AuthController> logger, LoginUseCase loginUseCase, RegisterUseCase registerUseCase, ConfirmUserEmailUseCase confirmUserEmailUseCase, ForgotPasswordUseCase forgotPasswordUseCase, ConfirmForgotPasswordCodeUseCase confirmForgotPasswordCodeUseCase)
        {
            _logger = logger;
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
            _confirmUserEmailUseCase = confirmUserEmailUseCase;
            _forgotPasswordUseCase = forgotPasswordUseCase;
            _confirmForgotPasswordCodeUseCase = confirmForgotPasswordCodeUseCase;
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
            try
            {
                await _registerUseCase.Register(request.userName, request.email, request.password);
                return Ok("Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar usuario: {ex.Message}");
            }
        }

        [HttpPost("confirmEmailVerification")]
        public async Task<IActionResult> ConfirmEmailVerification([FromBody] string username, string email, string confirmationCode)
        {
            bool isSuccess = await _confirmUserEmailUseCase.ConfirmEmailVerification(username, email, confirmationCode);

            if (isSuccess)
            {
                return Ok(new { Message = "Se ha verificado el Email correctamente. Ya puede ingresar al sitio." });
            }
            else
            {
                return BadRequest(new { Message = "Ocurrió un error al querer validar el email, intentelo nuevamente." });
            }
        }

        [HttpPost("passwordRecover")]
        public async Task<IActionResult> PasswordEmailRecover([FromBody] string username)
        {
            try
            {
                await _forgotPasswordUseCase.ForgotPasswordEmailAsync(username);
                return Ok("Usuario registrado exitosamente");
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Error al enviar el correo de recuperación: {ex.Message}");
            }
        }

        [HttpPost("passwordRecoverCode")]
        public async Task<IActionResult> ForgotPasswordCodeRecover([FromBody] string username, string confirmationCode, string password)
        {
            try
            {
                await _confirmForgotPasswordCodeUseCase.ConfirmForgotPasswordAsync(username, confirmationCode, password);
                return Ok("Contraseña cambiada exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cambiar la contraseña: {ex.Message}");
            }
        }
    }
}
