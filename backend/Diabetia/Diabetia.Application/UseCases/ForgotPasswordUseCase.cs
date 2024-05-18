using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.UseCases
{
    public class ForgotPasswordUseCase
    {
    
        private readonly IApiCognitoProvider _apiCognitoProvider;
        public ForgotPasswordUseCase(IApiCognitoProvider apiCognitoProvider)
        {
            _apiCognitoProvider = apiCognitoProvider;
        }

        public async Task ForgotPasswordEmailAsync(string username)
        {
            await _apiCognitoProvider.ForgotPasswordRecoverAsync(username);
        }
    }
}
