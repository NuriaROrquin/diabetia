using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class ConfirmUserEmailUseCase
    {
        private readonly IApiCognitoProvider _apiCognitoProvider;
        private readonly IAuthRepository _authRepository;
        public ConfirmUserEmailUseCase(IApiCognitoProvider apiCognitoProvider, IAuthRepository authRepository)
            {
                _apiCognitoProvider = apiCognitoProvider;
                _authRepository = authRepository;
            }

            public async Task<bool> ConfirmEmailVerification(string username, string email, string confirmationCode)
            {
                string hashCode = await _authRepository.GetUserHashAsync(email);
                bool response = await _apiCognitoProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode);
                return response;
            }
    }
}
