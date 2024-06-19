using Diabetia.Domain.Utilities.Interfaces;
using Diabetia.Domain.Models;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.AuthUseCases
{
    public class AuthForgotPasswordUseCase
    {

        private readonly IAuthProvider _apiCognitoProvider;
        private readonly IEmailValidator _emailValidator;
        private readonly IUsernameDBValidator _usernameDBValidator;
        private readonly IUserStatusValidator _userStatusValidator;
        public AuthForgotPasswordUseCase(IAuthProvider apiCognitoProvider, IEmailValidator emailValidator, IUsernameDBValidator usernameDBValidator, IUserStatusValidator userStatusValidator)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _emailValidator = emailValidator;
            _usernameDBValidator = usernameDBValidator;
            _userStatusValidator = userStatusValidator;
        }

        public async Task ForgotPasswordEmailAsync(Usuario user)
        {
            _emailValidator.IsValidEmail(user.Email);
            var username = await _usernameDBValidator.GetUsernameByEmail(user.Email);
            await _userStatusValidator.CheckUserStatus(user.Email);

            await _apiCognitoProvider.ForgotPasswordRecoverAsync(username);
        }

        public async Task ConfirmForgotPasswordAsync(Usuario user, string confirmationCode, string password)
        {
            _emailValidator.IsValidEmail(user.Email);
            var username = await _usernameDBValidator.GetUsernameByEmail(user.Email);
            await _apiCognitoProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password);
        }
    }
}
