using Diabetia.Domain.Repositories;

namespace Diabetia.Application.UseCases
{
    public class EventPhysicalActivityUseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventPhysicalActivityUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddPhysicalEvent(string Email, int KindEvent, DateTime EventDate, String FreeNote, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime)
        {
            await _eventRepository.AddPhysicalActivityEvent(Email, KindEvent, EventDate, FreeNote, PhysicalActivity, IniciateTime, FinishTime);
        }

        public async Task EditPhysicalEvent(string Email, int EventId, DateTime EventDate, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime, string FreeNote)
        {
            await _eventRepository.EditPhysicalActivityEvent(Email, EventId, EventDate, PhysicalActivity, IniciateTime, FinishTime, FreeNote);
        }
    }
}
