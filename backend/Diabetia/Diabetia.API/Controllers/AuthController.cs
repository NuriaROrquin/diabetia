using Diabetia.API.DTO.AuthRequest;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Services;
using Diabetia.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(AuthLoginUseCase loginUseCase, AuthRegisterUseCase registerUseCase, AuthForgotPasswordUseCase forgotPasswordUseCase, AuthChangePasswordUseCase changePasswordUseCase, IJwtTokenService jwtTokenService)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
            _forgotPasswordUseCase = forgotPasswordUseCase;
            _changePasswordUseCase = changePasswordUseCase;
            _jwtTokenService = jwtTokenService;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthLoginRequest request)
        {
            var user = await _loginUseCase.UserLoginAsync(request.userInput, request.password);

            if (user.Token != null)
            {
                AuthLoginResponse res = new AuthLoginResponse();

                res.Token = _jwtTokenService.GenerateToken(user.Id.ToString(), user.Username, user.Email, user.StepCompleted);

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] AuthRegisterRequest request)
        {
            await _registerUseCase.Register(request.Username, request.Email, request.Password);
            return Ok("Usuario registrado exitosamente");
        }

        [HttpPost("confirmEmailVerification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmEmailVerificationAsync([FromBody] AuthConfirmEmailRequest request)
        {
            bool isSuccess = await _registerUseCase.ConfirmEmailVerification(request.Username, request.Email, request.ConfirmationCode);
            Console.Write("hola");
            if (isSuccess)
            {
                return Ok(new { Message = "Se ha verificado el Email correctamente. Ya puede ingresar al sitio." });
            }
            return BadRequest(new { Message = "Ocurri� un error al querer validar el email, intentelo nuevamente." });
        }

        [HttpPost("changePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeUserPasswordAsync([FromBody] AuthChangePasswordRequest request)
        {
            await _changePasswordUseCase.ChangeUserPasswordAsync(request.AccessToken, request.PreviousPassword, request.NewPassword);
            return Ok("Contrase�a cambiada exitosamente");
        }
        
        [HttpPost("passwordRecover")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PasswordEmailRecoverAsync([FromBody] AuthForgotPasswordRequest request)
        {
            await _forgotPasswordUseCase.ForgotPasswordEmailAsync(request.Email);
            return Ok("C�digo enviado exitosamente, revise su casilla de correo.");
        }

        [HttpPost("passwordRecoverCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgotPasswordCodeRecoverAsync([FromBody] AuthConfirmPasswordRecoverRequest request)
        {
            await _forgotPasswordUseCase.ConfirmForgotPasswordAsync(request.Email, request.ConfirmationCode, request.Password);
            return Ok("Contrase�a cambiada exitosamente");
        }

    }
}
