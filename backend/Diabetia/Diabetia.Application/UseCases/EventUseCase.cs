using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Common.Utilities;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;

namespace Diabetia.Application.UseCases
{
    public class EventUseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<string> GetEventType(int id)
        {
            return await _eventRepository.GetEventType(id);
        }

    }
}
