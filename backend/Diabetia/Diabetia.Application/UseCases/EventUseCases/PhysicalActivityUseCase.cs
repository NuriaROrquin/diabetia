using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.EventUseCases
{
    public class PhysicalActivityUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;
        private readonly IPatientEventValidator _patientEventValidator;
        private readonly IUserRepository _userRepository;

        public PhysicalActivityUseCase(IEventRepository eventRepository, IPatientValidator patientValidator, IPatientEventValidator patientEventValidator, IUserRepository userRepository)
        {
            _patientValidator = patientValidator;
            _eventRepository = eventRepository;
            _patientEventValidator = patientEventValidator;
            _userRepository = userRepository;
        }

        public async Task AddPhysicalEventAsync(string email, EventoActividadFisica physicalActivity)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            await _eventRepository.AddPhysicalActivityEventAsync(patient.Id, physicalActivity);
        }

        public async Task EditPhysicalEventAsync(string email, EventoActividadFisica physicalActivity)
        {
            await _patientValidator.ValidatePatient(email);
            var cargaEvento = await _eventRepository.GetEventByIdAsync(physicalActivity.IdCargaEventoNavigation.Id);
            await _patientEventValidator.ValidatePatientEvent(email, cargaEvento);
            await _eventRepository.EditPhysicalActivityEventAsync(physicalActivity);
        }
    }
}
