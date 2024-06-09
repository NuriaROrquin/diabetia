using Diabetia.Domain.Repositories;

namespace Diabetia.API.Controllers
{
    public class EventInsulinUseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventInsulinUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task AddInsulinEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, int Insulin)
        {
            await _eventRepository.AddInsulinEvent(Email, IdKindEvent, EventDate, FreeNote, Insulin);
        }
    }
}