using Diabetia.Domain.Repositories;

namespace Diabetia.API.Controllers
{
    public class AddInsulinEventUseCase
    {
        private readonly IEventRepository _eventRepository;

        public AddInsulinEventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task AddInsulinEvent(string Email, int IdKindEvent, DateTime EventDate, String FreeNote, int Insulin)
        {
            await _eventRepository.AddInsulinEvent(Email, IdKindEvent, EventDate, FreeNote, Insulin);
        }
    }
}