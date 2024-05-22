using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class ConfirmForgotPasswordCodeUseCase
    {
        private readonly IApiCognitoProvider _apiCognitoProvider;
        public ConfirmForgotPasswordCodeUseCase(IApiCognitoProvider apiCognitoProvider)
        {
            _apiCognitoProvider = apiCognitoProvider;
        }

        public async Task ConfirmForgotPasswordAsync(string username, string confirmationCode, string password)
        {
            await _apiCognitoProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password);
        }
    }
}
