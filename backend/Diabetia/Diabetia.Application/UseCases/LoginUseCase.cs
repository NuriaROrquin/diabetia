using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IAuthService _authService;
        private readonly IApiCognitoProvider _apiCognitoProvider;

        public LoginUseCase(IAuthService authService, IApiCognitoProvider apiCognitoProvider)
        {
            _authService = authService;
            _apiCognitoProvider = apiCognitoProvider;
        }

        public string Login(string email)
        {
            // Lógica para realizar la autenticación con cognito
            // Se conecta a provider de infrastructure para conexión con cognito

            var isCognitoSuccess = true;

            if(isCognitoSuccess)
            {
                return _authService.GenerateJwtToken(email);
            }

            return "";
        }

        public async Task<string> Register(string username, string email, string password)
        {
            string res = await _apiCognitoProvider.RegisterUserAsync(username, password, email);

            return res;
        }
    }
}
