using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
