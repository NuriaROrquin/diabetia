﻿using Diabetia.Application.Exceptions;
using Diabetia.Common.Utilities.Interfaces;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases
{
    public class AuthForgotPasswordUseCase
    {
    
        private readonly IAuthProvider _apiCognitoProvider;
        private readonly IAuthRepository _authRepository;
        private readonly IEmailValidator _emailValidator;
        private readonly IUsernameDBValidator _usernameDBValidator;
        private readonly IUserStatusValidator _userStatusValidator;
        public AuthForgotPasswordUseCase(IAuthProvider apiCognitoProvider, IAuthRepository authRepository, IEmailValidator emailValidator, IUsernameDBValidator usernameDBValidator, IUserStatusValidator userStatusValidator)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _authRepository = authRepository;
            _emailValidator = emailValidator;
            _usernameDBValidator = usernameDBValidator;
            _userStatusValidator = userStatusValidator;
        }

        public async Task ForgotPasswordEmailAsync(Usuario user)
        {
            _emailValidator.IsValidEmail(user.Email);
            var username = await _usernameDBValidator.CheckUsernameOnDB(user.Email);
            await _userStatusValidator.checkUserStatus(user.Email);

            await _apiCognitoProvider.ForgotPasswordRecoverAsync(username);
        }

        public async Task ConfirmForgotPasswordAsync(string email, string confirmationCode, string password)
        {
            _emailValidator.IsValidEmail(email);
            string username = await _authRepository.GetUsernameByEmailAsync(email);
            if (username == "")
            {
                throw new UsernameNotFoundException();
            }
            await _apiCognitoProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password);
        }
    }
}
