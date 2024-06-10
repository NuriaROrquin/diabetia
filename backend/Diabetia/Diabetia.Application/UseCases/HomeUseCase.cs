using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Common.Utilities;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Entities.Events;

namespace Diabetia.Application.UseCases
{
    public class HomeUseCase
    {
        private readonly IHomeRepository _homeRepository;
        private readonly IEventRepository _eventRepository;

        public HomeUseCase(IHomeRepository homeRepository, IEventRepository eventRepository)
        {
            _homeRepository = homeRepository;
            _eventRepository = eventRepository;
        }

        public async Task<Metrics> ShowMetrics(string Email, DateFilter? dateFilter)
        {
            Metrics metrics = new Metrics();

            metrics.PhysicalActivity = await _homeRepository.GetPhysicalActivity(Email, (int)TypeEventEnum.ACTIVIDADFISICA, dateFilter);

            metrics.Carbohydrates = await _homeRepository.GetChMetrics(Email, (int)TypeEventEnum.COMIDA, dateFilter);

            metrics.Glycemia = await _homeRepository.GetGlucose(Email, (int)TypeEventEnum.GLUCOSA, dateFilter);

            metrics.Hyperglycemia =  await _homeRepository.GetHyperglycemia(Email, dateFilter);

            metrics.Hypoglycemia = await _homeRepository.GetHypoglycemia(Email, dateFilter);

            metrics.Insulin = await _homeRepository.GetInsulin(Email, (int)TypeEventEnum.INSULINA, dateFilter);

            return metrics;
        }

        public async Task<Timeline> GetTimeline(string email)
        {
            var timeline = new Timeline();

            var lastEvents = await _homeRepository.GetLastEvents(email);

            Timeline items = new Timeline();

            foreach ( var lastEvent in lastEvents )
            {
                var type = await _eventRepository.GetEventType(lastEvent.Id);

                switch (type)
                {
                    case TypeEventEnum.GLUCOSA:
                        var glucose = await _eventRepository.GetGlucoseEventById(lastEvent.Id);
                        items.Items.Add(new TimelineItem
                        {
                            Title = glucose.Title + " " + (int)glucose.GlucoseLevel,
                            DateTime = glucose.DateEvent
                        });
                        break;
                    case TypeEventEnum.INSULINA:
                        var insulin = await _eventRepository.GetInsulinEventById(lastEvent.Id);
                        items.Items.Add(new TimelineItem
                        {
                            Title = insulin.Title,
                            DateTime = insulin.DateEvent 
                        });
                        break;
                    case TypeEventEnum.COMIDA:
                        var food = await _eventRepository.GetFoodEventById(lastEvent.Id);
                        items.Items.Add(new TimelineItem
                        {
                            Title = food.Title + " - " + food.IngredientName,
                            DateTime = food.DateEvent
                        });
                        break;
                    case TypeEventEnum.ACTIVIDADFISICA:
                        var physicalActivity = await _eventRepository.GetPhysicalActivityById(lastEvent.Id);
                        items.Items.Add(new TimelineItem
                        {
                            Title = physicalActivity.Title + " " + physicalActivity.Duration + "min",
                            DateTime = physicalActivity.DateEvent
                        });
                        break;
                    case TypeEventEnum.EVENTODESALUD:
                        var healthEvent = await _eventRepository.GetHealthEventById(lastEvent.Id);
                        items.Items.Add(new TimelineItem
                        {
                            Title = healthEvent.Title,
                            DateTime = healthEvent.DateEvent
                        });
                        break;
                    case TypeEventEnum.VISITAMEDICA:
                        var medicalVisitEvent = await _eventRepository.GetMedicalVisitEventById(lastEvent.Id);
                        items.Items.Add(new TimelineItem
                        {
                            Title = medicalVisitEvent.Title,
                            DateTime = medicalVisitEvent.DateEvent
                        });
                        break;
                    case TypeEventEnum.ESTUDIOS:
                        var examEvent = await _eventRepository.GetExamEventById(lastEvent.Id);
                        items.Items.Add(new TimelineItem
                        {
                            Title = examEvent.Title,
                            DateTime = examEvent.DateEvent
                        });
                        break;
                    case TypeEventEnum.NOTALIBRE:
                        break;
                    default:
                        break;
                }
            }

            return items;
        }

    }
}
