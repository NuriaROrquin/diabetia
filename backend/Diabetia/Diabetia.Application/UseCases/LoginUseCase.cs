using Diabetia.Domain.Entities;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IApiCognitoProvider _apiCognitoProvider;

        public LoginUseCase(IApiCognitoProvider apiCognitoProvider, IUserRepository userRepository)
        {
            _apiCognitoProvider = apiCognitoProvider;
            _userRepository = userRepository;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = new User
            {
                Token = await _apiCognitoProvider.LoginUserAsync(username, password),

                InformationCompleted = await _userRepository.GetInformationCompleted(username)
            };

            return user;            
        }        
    }
}
