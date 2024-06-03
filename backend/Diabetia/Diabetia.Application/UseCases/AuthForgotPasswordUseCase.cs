using Diabetia.Application.Exceptions;
using Diabetia.Common.Utilities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class AuthForgotPasswordUseCase
    {
    
        private readonly IAuthProvider _apiCognitoProvider;
        private readonly IAuthRepository _authRepository;
        public AuthForgotPasswordUseCase(IAuthProvider apiCognitoProvider, IAuthRepository authRepository)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _authRepository = authRepository;
        }

        public async Task ForgotPasswordEmailAsync(string email)
        {
            if (!EmailValidator.IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }
            string username = await _authRepository.GetUsernameByEmail(email);
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

        public async Task ConfirmForgotPasswordAsync(string username, string confirmationCode, string password)
        {
            bool checkUser = await _authRepository.CheckUsernameOnDatabase(username);
            if (!checkUser)
            {
                throw new UsernameNotFoundException();
            }
            await _apiCognitoProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password);
        }
    }
}
