using Diabetia.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.UseCases
{
    public class EventFoodManuallyUseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventFoodManuallyUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
    }
}
