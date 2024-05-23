using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class ForgotPasswordUseCase
    {
    
        private readonly IAuthProvider _apiCognitoProvider;
        public ForgotPasswordUseCase(IAuthProvider apiCognitoProvider)
        {
            _apiCognitoProvider = apiCognitoProvider;
        }

        public async Task ForgotPasswordEmailAsync(string username)
        {
            await _apiCognitoProvider.ForgotPasswordRecoverAsync(username);
        }

        public async Task ConfirmForgotPasswordAsync(string username, string confirmationCode, string password)
        {
            await _apiCognitoProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password);
        }
    }
}
