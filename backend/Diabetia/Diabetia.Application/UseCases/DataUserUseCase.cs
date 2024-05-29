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

        public async Task SecondStep(int typeDiabetes, bool useInsuline, string typeInsuline, string email)
        {
            await _userRepository.UpdateUserInfo(typeDiabetes, useInsuline, typeInsuline, email);
        }

        public async Task ThirdStep(string email, bool haceActividadFisica, int frecuencia, int idActividadFisica)
        {

            await _userRepository.CompletePhysicalUserInfo(email, haceActividadFisica, frecuencia, idActividadFisica);
        }
    }
}
