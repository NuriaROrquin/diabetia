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

        public async Task<string> Login(string username, string password)
        {
            try
            {
                var response = await _apiCognitoProvider.LoginUserAsync(username, password);
                if (response != null){
                    return response;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        
    }
}
