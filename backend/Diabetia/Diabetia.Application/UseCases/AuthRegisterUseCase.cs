using Diabetia.Application.Exceptions;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases
{
    public class AuthRegisterUseCase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAuthProvider _apiCognitoProvider;
        private readonly IEmailValidator _emailValidator;
        public AuthRegisterUseCase(IAuthProvider apiCognitoProvider, IAuthRepository authRepository, IEmailValidator emailValidator)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _authRepository = authRepository;
            _emailValidator = emailValidator;
        }
        public async Task Register(string username, string email, string password)
        {
            if (!_emailValidator.IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }
            if (await _authRepository.CheckEmailOnDatabaseAsync(email))
            {
                throw new EmailAlreadyExistsException();
            }
            string hashCode = await _apiCognitoProvider.RegisterUserAsync(username, password, email);
            await _authRepository.SaveUserHashAsync(username,email,hashCode);
            await _authRepository.SaveUserUsernameAsync(email, username);
        }
        
        public async Task<bool> ConfirmEmailVerification(string username, string email, string confirmationCode)
        {
            if (!_emailValidator.IsValidEmail(email))
            {
                throw new InvalidEmailException();
            }

            string hashCode = await _authRepository.GetUserHashAsync(email);
            if (string.IsNullOrEmpty(hashCode)) 
            {
                throw new InvalidOperationException();
            }
            bool response = await _apiCognitoProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode);
            await _authRepository.SetUserStateActiveAsync(email);
            return response;
        }
    }
}
 