using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases.AuthUseCases
{
    public class AuthChangePasswordUseCase
    {
        private readonly IAuthProvider _apiCognitoProvider;
        public AuthChangePasswordUseCase(IAuthProvider apiCognitoProvider)
        {
            _apiCognitoProvider = apiCognitoProvider;
        }

        public async Task ChangeUserPasswordAsync(string accessToken, string previousPassword, string newPassword)
        {
            await _apiCognitoProvider.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword);
        }
    }
}
