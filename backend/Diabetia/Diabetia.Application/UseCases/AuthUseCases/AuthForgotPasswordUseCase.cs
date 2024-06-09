using Diabetia.Application.Exceptions;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.AuthUseCases
{
    public class AuthForgotPasswordUseCase
    {

        private readonly IAuthProvider _apiCognitoProvider;
        private readonly IAuthRepository _authRepository;
        private readonly IEmailValidator _emailValidator;
        public AuthForgotPasswordUseCase(IAuthProvider apiCognitoProvider, IAuthRepository authRepository, IEmailValidator emailValidator)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _authRepository = authRepository;
            _emailValidator = emailValidator;
        }

        public async Task ForgotPasswordEmailAsync(string email)
        {
            if (!_emailValidator.IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }
            string username = await _authRepository.GetUsernameByEmailAsync(email);
            if (username == "")
            {
                throw new UsernameNotFoundException();
            }
            bool userState = await _authRepository.GetUserStateAsync(email);
            if (!userState)
            {
                throw new UserNotAuthorizedException();
            }
            await _apiCognitoProvider.ForgotPasswordRecoverAsync(username);
        }

        public async Task ConfirmForgotPasswordAsync(string email, string confirmationCode, string password)
        {
            if (!_emailValidator.IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }
            string username = await _authRepository.GetUsernameByEmailAsync(email);
            if (username == "")
            {
                throw new UsernameNotFoundException();
            }
            await _apiCognitoProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password);
        }
    }
}
