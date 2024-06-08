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

        public async Task EditInsulinEvent(int IdEvent, string Email, DateTime EventDate, string FreeNote, int Insulin)
        {
            await _eventRepository.EditInsulinEvent(IdEvent, Email, EventDate, FreeNote, Insulin);
        }
        public async Task DeleteInsulinEvent(int IdEvent)
        {
            await _eventRepository.DeleteInsulinEvent(IdEvent);
        }
    }
}