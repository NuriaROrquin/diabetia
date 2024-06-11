using Amazon.Runtime.Internal;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.UseCases
{
    public class EventFoodUseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventFoodUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<float> AddFoodManuallyEvent(string Email, DateTime EventDate, int IdKindEvent, IEnumerable<Ingredient> ingredients, string FreeNote)
        {
            var foodManuallyResponse = await _eventRepository.AddFoodManuallyEvent(Email, EventDate, IdKindEvent, ingredients, FreeNote);
            return foodManuallyResponse;
        }

        public async Task EditFoodManuallyEvent(int idEvent, string Email, DateTime EventDate, int IdKindEvent, IEnumerable<Ingredient> ingredients, string FreeNote)
        {
            await _eventRepository.EditFoodManuallyEvent(idEvent, Email, EventDate, IdKindEvent, ingredients, FreeNote);
        }

        public async Task AddFoodByTagEvent(string email, DateTime eventDate, int carbohydrates)
        {
            await _eventRepository.AddFoodByTagEvent(email, eventDate, carbohydrates);
        }
        
    }
}
