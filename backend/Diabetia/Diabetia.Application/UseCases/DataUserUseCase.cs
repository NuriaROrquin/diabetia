using Amazon.Runtime.Internal;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class DataUserUseCase
    {
        private readonly IAuthService _authService;
        private readonly IApiCognitoProvider _apiCognitoProvider;
        private readonly IUserRepository _userRepository;
        public DataUserUseCase(IAuthService authService, IApiCognitoProvider apiCognitoProvider)
        {
            _authService = authService;
            _apiCognitoProvider = apiCognitoProvider;
        }
        public async Task FirstStep(string name, string email, string gender, string lastname, int weight, string phone)
        {
             await _userRepository.CompleteUserInfo(name, email, gender, lastname, weight, phone); 

        }
    }
}
