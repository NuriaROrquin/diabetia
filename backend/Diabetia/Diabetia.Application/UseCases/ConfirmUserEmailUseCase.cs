using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class ConfirmUserEmailUseCase
    {
            private readonly IApiCognitoProvider _apiCognitoProvider;
            public ConfirmUserEmailUseCase(IApiCognitoProvider apiCognitoProvider)
            {
                _apiCognitoProvider = apiCognitoProvider;
            }

            public async Task<bool> ConfirmEmailVerification(string username, string confirmationCode)
            {
                bool response = await _apiCognitoProvider.ConfirmEmailVerificationAsync(username,confirmationCode);
                return response;
            }
    }
}
