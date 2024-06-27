using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.EventUseCases
{
    public class MedicalVisitUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;
        private readonly IPatientEventValidator _patientEventValidator;
        private readonly IUserRepository _userRepository;
        public MedicalVisitUseCase(IEventRepository eventRepository, IPatientValidator patientValidator, IPatientEventValidator patientEventValidator, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _patientValidator = patientValidator;
            _patientEventValidator = patientEventValidator;
            _userRepository = userRepository;
        }

        public async Task AddMedicalVisitEventAsync(string email, EventoVisitaMedica medicalVisit)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            await _eventRepository.AddMedicalVisitEventAsync(patient.Id, medicalVisit);
        }

        public async Task EditMedicalVisitEventAsync(string email, EventoVisitaMedica medicalVisit)
        {
            await _patientValidator.ValidatePatient(email);
            var @event = await _eventRepository.GetEventByIdAsync(medicalVisit.IdCargaEventoNavigation.Id);
            await _patientEventValidator.ValidatePatientEvent(email, @event);
            await _eventRepository.EditMedicalVisitEventAsync(medicalVisit);
        }
    }
}