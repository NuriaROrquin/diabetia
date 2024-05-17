using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class DataUserUseCase
    {
        private readonly IAuthService _authService;
        private readonly IApiCognitoProvider _apiCognitoProvider;
        public DataUserUseCase(IAuthService authService, IApiCognitoProvider apiCognitoProvider)
        {
            _authService = authService;
            _apiCognitoProvider = apiCognitoProvider;
        }
        public async Task<string> firstStep(string username, string password, string email)
        {
            string res = await _apiCognitoProvider.RegisterUserAsync(username, password, email);

            return res;
        }
    }
}
