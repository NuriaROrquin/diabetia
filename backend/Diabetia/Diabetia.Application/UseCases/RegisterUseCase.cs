using Diabetia.Application.Exceptions;
using Diabetia.Common.Utilities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using System.Data;

namespace Diabetia.Application.UseCases
{
    public class RegisterUseCase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAuthProvider _apiCognitoProvider;
        public RegisterUseCase(IAuthProvider apiCognitoProvider, IAuthRepository authRepository)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _authRepository = authRepository;
        }
        public async Task Register(string username, string email, string password)
        {
            string hashCode = await _apiCognitoProvider.RegisterUserAsync(username, password, email);
            await _authRepository.SaveUserHashAsync(username,email,hashCode);
        }
        public async Task<bool> ConfirmEmailVerification(string username, string email, string confirmationCode)
        {
            if (!EmailValidator.IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }

            string hashCode = await _authRepository.GetUserHashAsync(email);
            if (string.IsNullOrEmpty(hashCode)) 
            {
                throw new InvalidOperationException();
            }
            bool response = await _apiCognitoProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode);
            return response;
        }
    }
}
 