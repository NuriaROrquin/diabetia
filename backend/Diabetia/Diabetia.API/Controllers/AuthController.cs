using Diabetia.API.DTO;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterUseCase _registerUseCase;
        private readonly ForgotPasswordUseCase _forgotPasswordUseCase;
        private readonly ChangePasswordUseCase _changePasswordUseCase;

        public AuthController(LoginUseCase loginUseCase, RegisterUseCase registerUseCase, ForgotPasswordUseCase forgotPasswordUseCase, ChangePasswordUseCase changePasswordUse)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
            _forgotPasswordUseCase = forgotPasswordUseCase;
            _changePasswordUseCase = changePasswordUse;
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
                return BadRequest("Usuario o contraseña invalidos");
            }
            
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _registerUseCase.Register(request.Username, request.Email, request.Password);
            return Ok("Usuario registrado exitosamente");
        }

        [HttpPost("confirmEmailVerification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmEmailVerification([FromBody] ConfirmEmailRequest request)
        {
            bool isSuccess = await _registerUseCase.ConfirmEmailVerification(request.Username, request.Email, request.ConfirmationCode);
            Console.Write("hola");
            if (isSuccess)
            {
                return Ok(new { Message = "Se ha verificado el Email correctamente. Ya puede ingresar al sitio." });
            }
            return BadRequest(new { Message = "Ocurrió un error al querer validar el email, intentelo nuevamente." });
        }

        [HttpPost("passwordRecover")]
        public async Task<IActionResult> PasswordEmailRecover([FromBody] UserRequest request)
        {
            await _forgotPasswordUseCase.ForgotPasswordEmailAsync(request.Username);
            return Ok("Usuario registrado exitosamente");
        }

        [HttpPost("passwordRecoverCode")]
        public async Task<IActionResult> ForgotPasswordCodeRecover([FromBody] ConfirmPasswordRecoverRequest request)
        {
            await _forgotPasswordUseCase.ConfirmForgotPasswordAsync(request.Username, request.ConfirmationCode, request.Password);
            return Ok("Contraseña cambiada exitosamente");
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangeUserPasswordAsync([FromBody] ChangePasswordRequest request)
        {
            await _changePasswordUseCase.ChangeUserPasswordAsync(request.AccessToken, request.PreviousPassword, request.NewPassword);
            return Ok("Contraseña cambiada exitosamente");
        }
    }
}
