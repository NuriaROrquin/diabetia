using Diabetia.Domain.Repositories;

namespace Diabetia.Application.UseCases
{
    public class AddPhysicalEventUseCase
    {
        private readonly IEventRepository _eventRepository;

        public AddPhysicalEventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddPhysicalEvent(string Email, int KindEvent, DateTime EventDate, String FreeNote, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime)
        {
            await _eventRepository.AddPhysicalActivityEvent(Email, KindEvent, EventDate, FreeNote, PhysicalActivity, IniciateTime, FinishTime);
        }
    }
}
