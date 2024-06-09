using Diabetia.Domain.Entities;
using Diabetia.Domain.Models;

namespace Diabetia.Domain.Services
{
    public interface IUserRepository
    {
        public Task<Usuario> GetUserInfo(string userName);
        public Task CompleteUserInfo(string name, string email, string gender, string lastname, int weight, string phone, DateOnly birthdate);
        public Task UpdateUserInfo(int typeDiabetes, bool useInsuline, int? typeInsuline, string email, bool? needsReminder, int? frequency, string? hourReminder, int? insulinePerCH);
        public Task<bool> GetStatusInformationCompletedAsync(string username);
        public Task CompletePhysicalUserInfo(string email, bool haceActividadFisica, int frecuencia, int idActividadFisica, int duracion);
        public Task CompleteDeviceslUserInfo(string email, bool tieneDispositivo, int? idDispositivo, int? frecuencia);
        public Task<User> GetEditUserInfo(string email);
        public Task<Patient> GetPatientInfo(string email);
        public Task<Patient> GetPhysicalInfo(string email);
        public Task <User> GetUserInformationFromUsernameAsync(string username);
        public Task<Paciente> GetPatient(string email);
        public Task<Exercise_Patient> GetExerciseInfo(string email);
        public Task<Device_Patient> GetPatientDeviceInfo(string email);
    }
}
