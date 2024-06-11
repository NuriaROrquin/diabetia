using Diabetia.Application.Exceptions;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases
{
    public class AuthLoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IAuthProvider _apiCognitoProvider;
        private readonly IInputValidator _inputValidator;

        public AuthLoginUseCase(IAuthProvider apiCognitoProvider, IUserRepository userRepository, IAuthRepository authRepository, IInputValidator inputValidator)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _userRepository = userRepository;
            _authRepository = authRepository;
            _inputValidator = inputValidator;
        }

        public async Task<User> UserLoginAsync(string userInput, string password)
        {
            string username = null;
            bool userExists = false;

            if (_inputValidator.IsEmail(userInput))
            {
                username = await _authRepository.GetUsernameByEmailAsync(userInput);
                if (string.IsNullOrEmpty(username))
                {
                    throw new UsernameNotFoundException();
                }
                userExists = true;
            }
            else
            {
                userExists = await _authRepository.CheckUsernameOnDatabaseAsync(userInput);
                if (!userExists)
                {
                    throw new UsernameNotFoundException();
                }
                username = userInput;
            }

            var tokenResponse = await _apiCognitoProvider.LoginUserAsync(username, password);
            if (tokenResponse.AuthenticationResult == null || string.IsNullOrEmpty(tokenResponse.AuthenticationResult.AccessToken))
            {
                throw new UserNotAuthorizedException();
            }
            var userInformation = await _userRepository.GetUserInformationFromUsernameAsync(username);
            if (userInformation == null)
            {
                throw new NoInformationUserException();
            }
            User user = new User
            {
                Token = tokenResponse.AuthenticationResult.AccessToken,
                InitialFormCompleted = await _userRepository.GetStatusInformationCompletedAsync(username),
                Email = userInformation.Email,
                Username = username,
                Id = userInformation.Id,
                StepCompleted = userInformation.StepCompleted
            };

            return user;            
        }        
    }
}
