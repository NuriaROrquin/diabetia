using Diabetia.API;
using Diabetia.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IAuthService _authService;

        public LoginUseCase(IAuthService authService)
        {
            _authService = authService;
        }

        public string Login(LoginRequest request)
        {
            // Lógica para realizar la autenticación con cognito
            // Se conecta a provider de infrastructure para conexión con cognito

            var isCognitoSuccess = true;

            if(isCognitoSuccess)
            {
                return _authService.GenerateJwtToken(request.email);
            }

            return "";
        }
    }
}
