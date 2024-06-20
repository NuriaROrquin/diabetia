using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;


namespace Diabetia.Application.UseCases.EventUseCases
{
    public class InsulinUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;
        private readonly IPatientEventValidator _patientEventValidator;
        private readonly IUserRepository _userRepository;

        public InsulinUseCase(IEventRepository eventRepository, IPatientValidator patientValidator, IPatientEventValidator patientEventValidator, IUserRepository userRepository)
        {
            _patientValidator = patientValidator;
            _eventRepository = eventRepository;
            _patientEventValidator = patientEventValidator;
            _userRepository = userRepository;
        }

        public async Task AddInsulinEventAsync(string email, EventoInsulina insulin)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);          
            await _eventRepository.AddInsulinEventAsync(patient.Id, insulin);
        }

        /*
        public async Task EditInsulinEventAsync(string email, EventoInsulina insulin)
        {
            await _patientValidator.ValidatePatient(email);

            var eventId = insulin.IdCargaEventoNavigation.Id;
            var loadedEvent = await _eventRepository.GetEventByIdAsync(eventId);
            await _patientEventValidator.ValidatePatientEvent(email, loadedEvent);
            await _eventRepository.EditInsulinEventAsync(insulin);
        }
        */
    }
}
