using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Common.Utilities;
using Diabetia.Domain.Repositories;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using Diabetia.Domain.Entities.Events;

namespace Diabetia.Application.UseCases
{
    public class EventUseCase
    {
        private readonly IEventRepository _eventRepository;
        private object glucoseEvent;

        public EventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<GenericEvent> GetEvent(int id)
        {
            var type = await _eventRepository.GetEventType(id);

            switch (type)
            {
                case TypeEventEnum.GLUCOSA:
                    var glucose = _eventRepository.GetGlucoseEventById(id);
                    GenericEvent evento = new GenericEvent {
                    }

                    return evento;

            }

            return null;

        }
    }
}
