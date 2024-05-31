using Diabetia.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.UseCases
{
    public class ChangePasswordUseCase
    {
        private readonly IAuthProvider _apiCognitoProvider;
        public ChangePasswordUseCase(IAuthProvider apiCognitoProvider) 
        {
            _apiCognitoProvider = apiCognitoProvider;
        }

        public async Task ChangeUserPasswordAsync(string accessToken, string previousPassword, string newPassword)
        {
            await _apiCognitoProvider.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword);
        }
    }
}
