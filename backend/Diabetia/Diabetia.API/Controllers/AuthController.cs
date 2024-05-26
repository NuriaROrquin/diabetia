using Diabetia.API.DTO;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterUseCase _registerUseCase;
        private readonly ForgotPasswordUseCase _forgotPasswordUseCase;

        public AuthController(LoginUseCase loginUseCase, RegisterUseCase registerUseCase, ForgotPasswordUseCase forgotPasswordUseCase)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
            _forgotPasswordUseCase = forgotPasswordUseCase;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] LoginRequest request)
        {
            var user = await _loginUseCase.Login(request.username, request.password);
            if (user.Token != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Path = "/"
                };

                Response.Cookies.Append("jwt", user.Token, cookieOptions);

                LoginResponse res = new LoginResponse();

                res.InformationCompleted = user.InformationCompleted;

                return Ok(res);
            }
            else
            {
                return BadRequest("Usuario o contrase�a invalidos");
            }
            
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _registerUseCase.Register(request.userName, request.email, request.password);
            return Ok("Usuario registrado exitosamente");
        }

        [HttpPost("confirmEmailVerification")]
        public async Task<IActionResult> ConfirmEmailVerification([FromBody] UserRequest request)
        {
            bool isSuccess = await  _registerUseCase.ConfirmEmailVerification(request.username, request.email, request.confirmationCode);

            if (isSuccess)
            {
                return Ok(new { Message = "Se ha verificado el Email correctamente. Ya puede ingresar al sitio." });
            }
            else
            {
                return BadRequest(new { Message = "Ocurri� un error al querer validar el email, intentelo nuevamente." });
            }
        }

        [HttpPost("passwordRecover")]
        public async Task<IActionResult> PasswordEmailRecover([FromBody] UserRequest request)
        {
            await _forgotPasswordUseCase.ForgotPasswordEmailAsync(request.username);
            return Ok("Usuario registrado exitosamente");
        }

        [HttpPost("passwordRecoverCode")]
        public async Task<IActionResult> ForgotPasswordCodeRecover([FromBody] UserRequest request)
        {
            await _forgotPasswordUseCase.ConfirmForgotPasswordAsync(request.username, request.confirmationCode, request.password);
            return Ok("Contrase�a cambiada exitosamente");
        }
    }
}
