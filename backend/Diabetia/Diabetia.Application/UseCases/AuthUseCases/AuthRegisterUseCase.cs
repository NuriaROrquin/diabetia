﻿using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Utilities.Interfaces;
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
        private readonly IEmailDBValidator _emailDBValidator;
        public AuthRegisterUseCase(IAuthProvider apiCognitoProvider, IAuthRepository authRepository, IEmailValidator emailValidator, IEmailDBValidator emailDBValidator)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _authRepository = authRepository;
            _emailValidator = emailValidator;
            _emailDBValidator = emailDBValidator;
        }
        public async Task Register(Usuario user, string password)
        {
            _emailValidator.IsValidEmail(user.Email);
            await _emailDBValidator.CheckEmailOnDB(user.Email);

            string hashCode = await _apiCognitoProvider.RegisterUserAsync(user, password);
            await _authRepository.SaveUserHashAsync(user, hashCode);
            await _authRepository.SaveUserUsernameAsync(user);
        }

        public async Task<bool> ConfirmEmailVerification(Usuario user, string confirmationCode)
        {
            _emailValidator.IsValidEmail(user.Email);

            string hashCode = await _authRepository.GetUserHashAsync(user.Email);
            if (string.IsNullOrEmpty(hashCode))
            {
                throw new InvalidOperationException();
            }
            bool response = await _apiCognitoProvider.ConfirmEmailVerificationAsync(user.Username, hashCode, confirmationCode);
            await _authRepository.SetUserStateActiveAsync(user.Email);
            return response;
        }
    }
}