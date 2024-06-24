using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;


namespace Diabetia.Application.UseCases.EventUseCases
{
    public class GlucoseUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;
        private readonly IPatientEventValidator _patientEventValidator;
        private readonly IUserRepository _userRepository;

        public GlucoseUseCase(IEventRepository eventRepository, IPatientValidator patientValidator, IPatientEventValidator patientEventValidator, IUserRepository userRepository)
        {
            _patientValidator = patientValidator;
            _eventRepository = eventRepository;
            _patientEventValidator = patientEventValidator;
            _userRepository = userRepository;
        }

        public async Task AddGlucoseEventAsync(string email, EventoGlucosa glucose)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            await _eventRepository.AddGlucoseEventAsync(patient.Id, glucose);
        }
        
        public async Task EditGlucoseEventAsync(string email, EventoGlucosa glucose)
        {
            await _patientValidator.ValidatePatient(email);

            var eventId = glucose.IdCargaEventoNavigation.Id;
            var loadedEvent = await _eventRepository.GetEventByIdAsync(eventId);
            await _patientEventValidator.ValidatePatientEvent(email, loadedEvent);
            await _eventRepository.EditGlucoseEventAsync(glucose);
        }
    }
}
