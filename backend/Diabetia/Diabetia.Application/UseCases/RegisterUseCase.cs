using Diabetia.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.UseCases
{
    public class RegisterUseCase
    {
        private readonly IAuthService _authService;
        private readonly IApiCognitoProvider _apiCognitoProvider;
        public RegisterUseCase(IAuthService authService, IApiCognitoProvider apiCognitoProvider)
        {
            _authService = authService;
            _apiCognitoProvider = apiCognitoProvider;
        }
        public async Task<string> Register(string username, string email, string password)
        {
            string res = await _apiCognitoProvider.RegisterUserAsync(username, password, email);

            return res;
        }
    }
}
