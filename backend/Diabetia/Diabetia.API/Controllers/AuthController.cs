using Diabetia.API.DTO;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthLoginUseCase _loginUseCase;
        private readonly AuthRegisterUseCase _registerUseCase;
        private readonly AuthForgotPasswordUseCase _forgotPasswordUseCase;
        private readonly AuthChangePasswordUseCase _changePasswordUseCase;

        public AuthController(AuthLoginUseCase loginUseCase, AuthRegisterUseCase registerUseCase, AuthForgotPasswordUseCase forgotPasswordUseCase, AuthChangePasswordUseCase changePasswordUseCase)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
            _forgotPasswordUseCase = forgotPasswordUseCase;
            _changePasswordUseCase = changePasswordUseCase;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AuthLoginRequest request)
        {
            var user = await _loginUseCase.UserLoginAsync(request.username, request.password);

            if (user.Token != null)
            {
                AuthLoginResponse res = new AuthLoginResponse();

                res.InformationCompleted = user.InformationCompleted;
                res.Token = user.Token;
                res.Email = user.Email;

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
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request)
        {
            await _registerUseCase.Register(request.Username, request.Email, request.Password);
            return Ok("Usuario registrado exitosamente");
        }

        [HttpPost("confirmEmailVerification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmEmailVerification([FromBody] AuthConfirmEmailRequest request)
        {
            bool isSuccess = await _registerUseCase.ConfirmEmailVerification(request.Username, request.Email, request.ConfirmationCode);
            Console.Write("hola");
            if (isSuccess)
            {
                return Ok(new { Message = "Se ha verificado el Email correctamente. Ya puede ingresar al sitio." });
            }
            return BadRequest(new { Message = "Ocurriï¿½ un error al querer validar el email, intentelo nuevamente." });
        }

        [HttpPost("changePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeUserPasswordAsync([FromBody] AuthChangePasswordRequest request)
        {
            await _changePasswordUseCase.ChangeUserPasswordAsync(request.AccessToken, request.PreviousPassword, request.NewPassword);
            return Ok("Contraseï¿½a cambiada exitosamente");
        }
        
        [HttpPost("passwordRecover")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PasswordEmailRecover([FromBody] AuthForgotPasswordRequest request)
        {
            await _forgotPasswordUseCase.ForgotPasswordEmailAsync(request.Email);
            return Ok("Código enviado exitosamente, revise su casilla de correo.");
        }

        [HttpPost("passwordRecoverCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgotPasswordCodeRecover([FromBody] AuthConfirmPasswordRecoverRequest request)
        {
            await _forgotPasswordUseCase.ConfirmForgotPasswordAsync(request.Username, request.ConfirmationCode, request.Password);
            return Ok("Contraseï¿½a cambiada exitosamente");
        }

    }
}
