using Diabetia.Application.Exceptions;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class AuthLoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IAuthProvider _apiCognitoProvider;

        public AuthLoginUseCase(IAuthProvider apiCognitoProvider, IUserRepository userRepository, IAuthRepository authRepository)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _userRepository = userRepository;
            _authRepository = authRepository;
        }

        public async Task<User> UserLoginAsync(string username, string password)
        {
            var checkUser = await _authRepository.CheckUsernameOnDatabaseAsync(username);
            if (!checkUser)
            {
                throw new UsernameNotFoundException("Usuario no encontrado");
            }

            var tokenResponse = await _apiCognitoProvider.LoginUserAsync(username, password);
            if (tokenResponse.AuthenticationResult == null || string.IsNullOrEmpty(tokenResponse.AuthenticationResult.AccessToken))
            {
                throw new UserNotAuthorizedException();
            }
            var userInformation = await _userRepository.GetUserInformationFromUsernameAsync(username);
            if (userInformation == null)
            {
                throw new UsernameNotFoundException();
            }
            var email = userInformation.Email;
            User user = new User
            {
                Token = tokenResponse.AuthenticationResult.AccessToken,
                InformationCompleted = await _userRepository.GetStatusInformationCompletedAsync(username),
                Email = email
            };

            return user;            
        }        
    }
}
