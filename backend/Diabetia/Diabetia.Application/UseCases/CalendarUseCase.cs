using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Entities.Events;

namespace Diabetia.Application.UseCases
{
    public class CalendarUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public CalendarUseCase(IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<Dictionary<string, List<EventItem>>> GetAllEvents(string email)
        {
            var eventsByDate = new Dictionary<string, List<EventItem>>();

            var patient = await _userRepository.GetPatient(email);

            var physicalActivityEvents = await _eventRepository.GetPhysicalActivity(patient.Id);
            var foodEvents = await _eventRepository.GetFoods(patient.Id);
            var examEvents = await _eventRepository.GetExams(patient.Id);
            var glucoseEvents = await _eventRepository.GetGlycemia(patient.Id);
            var insulinEvents = await _eventRepository.GetInsulin(patient.Id);
            var healthEvents = await _eventRepository.GetHealth(patient.Id);
            var medicalVisitEvents = await _eventRepository.GetMedicalVisit(patient.Id);


            var groupedFoodEvents = foodEvents
                .GroupBy(fe => new { fe.DateEvent })
                .Select(g => new
                {
                    g.Key.DateEvent,
                    Title = "Comida",
                    Ingredients = string.Join(", ", g.Select(fe => fe.IngredientName))
                });

            AddEventsToDictionary(eventsByDate, physicalActivityEvents, e => e.DateEvent, e => new EventItem
            {
                Time = e.DateEvent.ToString("hh:mm tt"),
                Title = e.Title
            });

            AddEventsToDictionary(eventsByDate, groupedFoodEvents, e => e.DateEvent, e => new EventItem
            {
                Time = e.DateEvent.ToString("hh:mm tt"),
                Title = e.Title,
                AdditionalInfo = e.Ingredients
            });

            AddEventsToDictionary(eventsByDate, examEvents, e => e.DateEvent, e => new EventItem
            {
                Time = e.DateEvent.ToString("hh:mm tt"),
                Title = e.Title
            });

            AddEventsToDictionary(eventsByDate, glucoseEvents, e => e.DateEvent, e => new EventItem
            {
                Time = e.DateEvent.ToString("hh:mm tt"),
                Title = e.Title,
                AdditionalInfo = e.GlucoseLevel.ToString()
            });

            AddEventsToDictionary(eventsByDate, insulinEvents, e => e.DateEvent, e => new EventItem
            {
                Time = e.DateEvent.ToString("hh:mm tt"),
                Title = e.Title,
                AdditionalInfo = $"Dosis: {e.Dosage} - {e.InsulinType}"
            });

            AddEventsToDictionary(eventsByDate, healthEvents, e => e.DateEvent, e => new EventItem
            {
                Time = e.DateEvent.ToString("hh:mm tt"),
                Title = e.Title
            });

            AddEventsToDictionary(eventsByDate, medicalVisitEvents, e => e.DateEvent, e => new EventItem
            {
                Time = e.DateEvent.ToString("hh:mm tt"),
                Title = e.Title,
                AdditionalInfo = e.Description
            });

            return eventsByDate;
        }

        private void AddEventsToDictionary<T>(Dictionary<string, List<EventItem>> dictionary, IEnumerable<T> events, Func<T, DateTime> getDate, Func<T, EventItem> createEventItem)
        {
            foreach (var evt in events)
            {
                string eventDate = getDate(evt).ToString("yyyy-MM-dd");

                if (!dictionary.ContainsKey(eventDate))
                {
                    dictionary[eventDate] = new List<EventItem>();
                }

                dictionary[eventDate].Add(createEventItem(evt));
            }
        }

    }
}
