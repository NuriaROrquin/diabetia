using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Models;
using Diabetia.Domain.Services;
using Amazon.Runtime.Internal;
using Diabetia.Domain.Repositories;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases
{
    public class DataUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPatientValidator _patientValidator;

        public DataUserUseCase(IUserRepository userRepository, IPatientValidator patienValidator)
        {
            _userRepository = userRepository;
            _patientValidator = patienValidator;
        }
        public async Task<Usuario> GetUserInfo(string userName)
        {
            return await _userRepository.GetUserInfo(userName);
        }
        public async Task<Paciente> FirstStep(string email, Paciente patient)
        {
            await _patientValidator.ValidatePatient(email);
            await _userRepository.CompleteUserInfo(patient);

            var patient_local = await _userRepository.GetPatient(email);

            return patient_local;
        }

        public async Task SecondStep(string email, Paciente patient)
        {
            await _patientValidator.ValidatePatient(email);
            await _userRepository.UpdateUserInfo(patient);
        }

        public async Task ThirdStep(string email, PacienteActividadFisica patient_actfisica)
        {
            await _patientValidator.ValidatePatient(email);
            await _userRepository.CompletePhysicalUserInfo(patient_actfisica);
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
