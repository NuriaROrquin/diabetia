using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;

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


            foreach (var physicalActivityEvent in physicalActivityEvents)
            {
                string eventDate = physicalActivityEvent.DateEvent.ToString("yyyy-MM-dd");

                if (!eventsByDate.ContainsKey(eventDate))
                {
                    eventsByDate[eventDate] = new List<EventItem>();
                }

                eventsByDate[eventDate].Add(new EventItem
                {
                    Time = physicalActivityEvent.DateEvent.ToString("hh:mm tt"),
                    Title = physicalActivityEvent.Title
                });
            }

            var groupedFoodEvents = foodEvents
                .GroupBy(fe => new { fe.DateEvent })
                .Select(g => new
                {
                    DateEvent = g.Key.DateEvent,
                    Title = "Comida",
                    Ingredients = string.Join(", ", g.Select(fe => fe.IngredientName))
                });

            foreach (var foodEvent in groupedFoodEvents)
            {
                string eventDate = foodEvent.DateEvent.ToString("yyyy-MM-dd");

                if (!eventsByDate.ContainsKey(eventDate))
                {
                    eventsByDate[eventDate] = new List<EventItem>();
                }

                eventsByDate[eventDate].Add(new EventItem
                {
                    Time = foodEvent.DateEvent.ToString("hh:mm tt"),
                    Title = foodEvent.Title,
                    AdditionalInfo = foodEvent.Ingredients
                });
            }

            foreach (var examEvent in examEvents)
            {
                string eventDate = examEvent.DateEvent.ToString("yyyy-MM-dd");

                if (!eventsByDate.ContainsKey(eventDate))
                {
                    eventsByDate[eventDate] = new List<EventItem>();
                }

                eventsByDate[eventDate].Add(new EventItem
                {
                    Time = examEvent.DateEvent.ToString("hh:mm tt"),
                    Title = examEvent.Title
                });
            }

            foreach (var glucoseEvent in glucoseEvents)
            {
                string eventDate = glucoseEvent.DateEvent.ToString("yyyy-MM-dd");

                if (!eventsByDate.ContainsKey(eventDate))
                {
                    eventsByDate[eventDate] = new List<EventItem>();
                }

                eventsByDate[eventDate].Add(new EventItem
                {
                    Time = glucoseEvent.DateEvent.ToString("hh:mm tt"),
                    Title = glucoseEvent.Title,
                    AdditionalInfo = glucoseEvent.GlucoseLevel.ToString()
                });
            }

            return eventsByDate;
        }

    }
}
