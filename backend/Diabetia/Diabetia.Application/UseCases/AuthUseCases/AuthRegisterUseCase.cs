using Diabetia.Application.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.AuthUseCases
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
        public async Task Register(Usuario user, string password)
        {
            _emailValidator.IsValidEmail(user.Email);
            if (await _authRepository.CheckEmailOnDatabaseAsync(user.Email))
            {
                throw new EmailAlreadyExistsException();
            }
            string hashCode = await _apiCognitoProvider.RegisterUserAsync(user.Username, password, user.Email);
            await _authRepository.SaveUserHashAsync(user.Username, user.Email, hashCode);
            await _authRepository.SaveUserUsernameAsync(user.Email, user.Username);
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
