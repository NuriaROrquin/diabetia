using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.EventUseCases
{
    public class FreeNoteUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;
        private readonly IUserRepository _userRepository;
        public FreeNoteUseCase(IEventRepository eventRepository, IPatientValidator patientValidator, IUserRepository userRepository) 
        {
            _eventRepository = eventRepository;
            _patientValidator = patientValidator;
            _userRepository = userRepository;   
        }

        public async Task AddFreeNoteEventAsync(string email, CargaEvento freeNoteEvent)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            await _eventRepository.AddFreeNoteEventAsync(patient.Id, freeNoteEvent);
        }
    }
}
