using Amazon.Runtime.Internal;
using Diabetia.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        public async Task AddFoodManuallyEvent(string Email, DateTime EventDate, int IdKindEvent, decimal Quantity, int IdIngredient, string FreeNote)
        {
            await _eventRepository.AddFoodManuallyEvent(Email, EventDate, IdKindEvent, Quantity, IdIngredient, FreeNote);
        }
    }
}
