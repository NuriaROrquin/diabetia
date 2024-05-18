using Amazon.Runtime.Internal;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class DataUserUseCase
    {
        private readonly IUserRepository _userRepository;
        public DataUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task FirstStep(string name, string email, string gender, string lastname, int weight, string phone)
        {
             await _userRepository.CompleteUserInfo(name, email, gender, lastname, weight, phone); 

        }
    }
}
