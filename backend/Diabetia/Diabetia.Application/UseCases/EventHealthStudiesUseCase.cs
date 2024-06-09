using Diabetia.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.UseCases
{
    public class EventHealthStudiesUseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventHealthStudiesUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddHealthStudiesEvent(string Email, int IdKindEvent, DateTime EventDate, string File, string HealthStudyName, bool? Reminder, DateTime? ReminderDate)
        {
            await _eventRepository.AddHealthStudiesEvent(Email, IdKindEvent, EventDate, File, HealthStudyName, Reminder, ReminderDate);
        }
        /*
        public async Task EditHealthStudiesEvent(int IdEvent, string Email, int IdKindEvent, DateTime EventDate, string File, string HealthStudyName, bool? Reminder, DateTime? ReminderDate)
        {
            await _eventRepository.EditHealthStudiesEvent(IdEvent, Email, EventDate, File, HealthStudyName, Reminder, ReminderDate);
        }
        public async Task DeleteHealthStudiesEvent(int IdEvent, string Email)
        {
            await _eventRepository.DeleteHealthStudiesEvent(IdEvent, Email);
        }*/
    }
}
