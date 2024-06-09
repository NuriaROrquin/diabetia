using Diabetia.Domain.Entities;
using Diabetia.Domain.Models;
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
        public async Task<Usuario> GetUserInfo(string userName)
        {
            return await _userRepository.GetUserInfo(userName);
        }
        public async Task FirstStep(string name, string email, string gender, string lastname, int weight, string phone, DateOnly birthdate)
        {
            await _userRepository.CompleteUserInfo(name, email, gender, lastname, weight, phone, birthdate);
        }

        public async Task SecondStep(int typeDiabetes, bool useInsuline, int typeInsuline, string email, bool needsReminder, int frequency, string hourReminder)
        {
            await _userRepository.UpdateUserInfo(typeDiabetes, useInsuline, typeInsuline, email, needsReminder, frequency, hourReminder);
        }

        public async Task ThirdStep(string email, bool haceActividadFisica, int frecuencia, int idActividadFisica, int duracion)
        {

            await _userRepository.CompletePhysicalUserInfo(email, haceActividadFisica, frecuencia, idActividadFisica, duracion);
        }

        public async Task FourthStep(string email, bool tieneDispositivo, int? idDispositivo, int? frecuencia)
        {
            await _userRepository.CompleteDeviceslUserInfo(email, tieneDispositivo, idDispositivo, frecuencia);
        }

        public async Task<User> GetEditUserInfo(string email)
        {
            return await _userRepository.GetEditUserInfo(email);
        }

        public async Task<Patient> GetPatientInfo(string email)
        {
            return await _userRepository.GetPatientInfo(email);
        }

        public async Task<Patient> GetPhysicalInfo(string email)
        {
            return await _userRepository.GetPhysicalInfo(email);
        }

        public async Task<Exercise_Patient> GetExerciseInfo(string email)
        {
            return await _userRepository.GetExerciseInfo(email);
        }

        public async Task<Device_Patient> GetPatientDeviceInfo(string email)
        {
            return await _userRepository.GetPatientDeviceInfo(email);
        }
    }
}
