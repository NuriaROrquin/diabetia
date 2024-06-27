using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Entities.Events;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases
{
    public class CalendarUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPatientValidator _patientValidator;

        public CalendarUseCase(IEventRepository eventRepository, IUserRepository userRepository, IPatientValidator patientValidator)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _patientValidator = patientValidator;
        }

        public async Task<Dictionary<string, List<EventItem>>> GetAllEvents(string email)
        {
            await _patientValidator.ValidatePatient(email);

            var eventsByDate = new Dictionary<string, List<EventItem>>();

            var patient = await _userRepository.GetPatient(email);

            var physicalActivityEvents = await _eventRepository.GetPhysicalActivity(patient.Id, null);
            var foodEvents = await _eventRepository.GetFoods(patient.Id, null);
            var examEvents = await _eventRepository.GetExams(patient.Id, null);
            var glucoseEvents = await _eventRepository.GetGlycemia(patient.Id, null);
            var insulinEvents = await _eventRepository.GetInsulin(patient.Id, null);
            var healthEvents = await _eventRepository.GetHealth(patient.Id, null);
            var medicalVisitEvents = await _eventRepository.GetMedicalVisit(patient.Id, null);


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
                Title = e.Title,
                AdditionalInfo = $"Duración: {e.Duration}min"
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

        public async Task<IEnumerable<EventItem>> GetAllEventsByDate(DateTime date, string email)
        {
            await _patientValidator.ValidatePatient(email);

            var patient = await _userRepository.GetPatient(email);

            var physicalActivityEvents = await _eventRepository.GetPhysicalActivity(patient.Id, date);
            var foodEvents = await _eventRepository.GetFoods(patient.Id, date);
            var examEvents = await _eventRepository.GetExams(patient.Id, date);
            var glucoseEvents = await _eventRepository.GetGlycemia(patient.Id, date);
            var insulinEvents = await _eventRepository.GetInsulin(patient.Id, date);
            var healthEvents = await _eventRepository.GetHealth(patient.Id, date);
            var medicalVisitEvents = await _eventRepository.GetMedicalVisit(patient.Id, date);

            var events = new List<EventItem>();

            foreach (var physicalActivityEvent in physicalActivityEvents)
            {
                var eventItem = new EventItem
                {
                    IdEvent = physicalActivityEvent.IdEvent,
                    Time = physicalActivityEvent.DateEvent.ToString("hh:mm tt"),
                    Title = physicalActivityEvent.Title,
                    AdditionalInfo = $"Duración: {physicalActivityEvent.Duration}min"
                };

                events.Add(eventItem);
            }

            foreach (var foodEvent in foodEvents)
            {
                var eventItem = new EventItem
                {
                    IdEvent = foodEvent.IdEvent,
                    Time = foodEvent.DateEvent.ToString("hh:mm tt"),
                    Title = "Comida",
                    AdditionalInfo = foodEvent.IngredientName != null ? $"Ingredientes: {foodEvent.IngredientName}" : "Etiqueta nutricional",
                };

                events.Add(eventItem);
            }

            foreach (var examEvent in examEvents)
            {
                var eventItem = new EventItem
                {
                    IdEvent = examEvent.IdEvent,
                    Time = examEvent.DateEvent.ToString("hh:mm tt"),
                    Title = examEvent.Title,
                };

                events.Add(eventItem);
            }

            foreach (var glucoseEvent in glucoseEvents)
            {
                var eventItem = new EventItem
                {
                    IdEvent = glucoseEvent.IdEvent,
                    Time = glucoseEvent.DateEvent.ToString("hh:mm tt"),
                    Title = glucoseEvent.Title,
                    AdditionalInfo = $"Nivel: {glucoseEvent.GlucoseLevel}mg/dL",
                };

                events.Add(eventItem);
            }

            foreach (var insulinEvent in insulinEvents)
            {
                var eventItem = new EventItem
                {
                    IdEvent = insulinEvent.IdEvent,
                    Time = insulinEvent.DateEvent.ToString("hh:mm tt"),
                    Title = insulinEvent.Title,
                };

                events.Add(eventItem);
            }

            foreach (var healthEvent in healthEvents)
            {
                var eventItem = new EventItem
                {
                    IdEvent = healthEvent.IdEvent,
                    Time = healthEvent.DateEvent.ToString("hh:mm tt"),
                    Title = healthEvent.Title,
                };

                events.Add(eventItem);
            }

            foreach (var medicalVisitEvent in medicalVisitEvents)
            {
                var eventItem = new EventItem
                {
                    IdEvent = medicalVisitEvent.IdEvent,
                    Time = medicalVisitEvent.DateEvent.ToString("hh:mm tt"),
                    Title = medicalVisitEvent.Title,
                    AdditionalInfo = medicalVisitEvent.Description,
                };

                events.Add(eventItem);
            }

            return events;
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
