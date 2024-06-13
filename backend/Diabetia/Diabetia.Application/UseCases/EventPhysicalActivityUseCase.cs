using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases
{
    public class EventPhysicalActivityUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;

        public EventPhysicalActivityUseCase(IEventRepository eventRepository, IPatientValidator patientValidator)
        {
            _patientValidator = patientValidator;
            _eventRepository = eventRepository;
        }

        public async Task AddPhysicalEventAsync(string email, EventoActividadFisica physicalActivity)
        {
            await _patientValidator.ValidatePatient(email);

            await _eventRepository.AddPhysicalActivityEventAsync(physicalActivity);
        }

        public async Task EditPhysicalEventAsync(string email, int EventId, DateTime EventDate, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime, string FreeNote)
        {
            await _patientValidator.ValidatePatient(email);

            await _eventRepository.EditPhysicalActivityEventAsync(email, EventId, EventDate, PhysicalActivity, IniciateTime, FinishTime, FreeNote);
        }
    }
}
