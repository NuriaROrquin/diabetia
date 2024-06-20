﻿using Diabetia.Domain.Entities;
using Diabetia.Domain.Models;

namespace Diabetia.Domain.Services
{
    public interface IUserRepository
    {
        public Task<Usuario> GetUserInfo(string userName);
        public Task CompleteUserInfo(Paciente patient);
        public Task UpdateUserInfo(Paciente patient);
        public Task<bool> GetStatusInformationCompletedAsync(string username);
        public Task CompletePhysicalUserInfo(PacienteActividadFisica patient_actfisica);
        public Task CompleteDeviceslUserInfo(DispositivoPaciente patient_dispo, bool tieneDispositivo);
        public Task<User> GetEditUserInfo(string email);
        public Task<Patient> GetPatientInfo(string email);
        public Task<Patient> GetPhysicalInfo(string email);
        public Task <User> GetUserInformationFromUsernameAsync(string username);
        public Task<Paciente> GetPatient(string email);
        public Task<Exercise_Patient> GetExerciseInfo(string email);
        public Task<Device_Patient> GetPatientDeviceInfo(string email);

    }
}
