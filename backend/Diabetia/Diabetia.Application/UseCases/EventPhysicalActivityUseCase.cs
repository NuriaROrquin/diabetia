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

        public async Task AddPhysicalEventAsync(string email, int KindEvent, DateTime EventDate, String FreeNote, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime)
        {
            await _patientValidator.ValidatePatient(email);

            await _eventRepository.AddPhysicalActivityEventAsync(email, KindEvent, EventDate, FreeNote, PhysicalActivity, IniciateTime, FinishTime);
        }

        public async Task EditPhysicalEventAsync(string email, int EventId, DateTime EventDate, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime, string FreeNote)
        {
            await _patientValidator.ValidatePatient(email);

            await _eventRepository.EditPhysicalActivityEventAsync(email, EventId, EventDate, PhysicalActivity, IniciateTime, FinishTime, FreeNote);
        }
    }
}
