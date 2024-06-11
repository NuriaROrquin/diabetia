﻿using Diabetia.Domain.Repositories;

namespace Diabetia.Application.UseCases.EventUseCases
{
    public class EventPhysicalActivityUseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventPhysicalActivityUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddPhysicalEventAsync(string Email, int KindEvent, DateTime EventDate, string FreeNote, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime)
        {
            await _eventRepository.AddPhysicalActivityEventAsync(Email, KindEvent, EventDate, FreeNote, PhysicalActivity, IniciateTime, FinishTime);
        }

        public async Task EditPhysicalEventAsync(string Email, int EventId, DateTime EventDate, int PhysicalActivity, TimeSpan IniciateTime, TimeSpan FinishTime, string FreeNote)
        {
            await _eventRepository.EditPhysicalActivityEventAsync(Email, EventId, EventDate, PhysicalActivity, IniciateTime, FinishTime, FreeNote);
        }
    }
}
