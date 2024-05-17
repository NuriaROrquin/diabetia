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
        private readonly IApiCognitoProvider _apiCognitoProvider;
        public RegisterUseCase(IApiCognitoProvider apiCognitoProvider)
        {
            _apiCognitoProvider = apiCognitoProvider;
        }
        public async Task<string> Register(string username, string email, string password)
        {
            string response = await _apiCognitoProvider.RegisterUserAsync(username, password, email);
            return response;
        }
    }
}
