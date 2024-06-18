using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Utilities.Interfaces;

namespace Diabetia.Domain.Utilities.Validations
{
    public class EventValidator : IEventValidator
    {
        private readonly IEventRepository _eventRepository;

        public EventValidator(IEventRepository eventRepository) 
        {
            _eventRepository = eventRepository;
        }
        public async Task checkEvent(int eventId)
        {
            var @event = await _eventRepository.GetEventByIdAsync(eventId);
            if (@event == null) 
            {
                throw new EventNotFoundException();
            }
        }
    }
}
