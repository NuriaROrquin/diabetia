using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Common.Utilities;
using Diabetia.Domain.Repositories;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using Diabetia.Domain.Entities.Events;

namespace Diabetia.Application.UseCases
{
    public class EventUseCase
    {
        private readonly IEventRepository _eventRepository;
        private object glucoseEvent;

        public EventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<GenericEvent?> GetEvent(int id)
        {
            var type = await _eventRepository.GetEventType(id);

            switch (type)
            {
                case TypeEventEnum.GLUCOSA:
                    var glucose = await _eventRepository.GetGlucoseEventById(id); 
                    return new GenericEvent
                    {
                        GlucoseEvent = glucose,
                        TypeEvent = glucose.IdEventType
                    };
                case TypeEventEnum.INSULINA:
                    var insulin = await _eventRepository.GetInsulinEventById(id);
                    return new GenericEvent
                    {
                        InsulinEvent = insulin,
                        TypeEvent = insulin.IdEventType
                    };
                case TypeEventEnum.COMIDA:
                    var food = await _eventRepository.GetFoodEventById(id);
                    return new GenericEvent
                    {
                        FoodEvent = food,
                        TypeEvent = food.IdEventType
                    };
                case TypeEventEnum.ACTIVIDADFISICA:
                    var physicalActivity = await _eventRepository.GetPhysicalActivityById(id);
                    return new GenericEvent
                    {
                        PhysicalActivityEvent = physicalActivity,
                        TypeEvent = physicalActivity.IdEventType
                    };
                case TypeEventEnum.EVENTODESALUD:
                    var healthEvent = await _eventRepository.GetHealthEventById(id);
                    return new GenericEvent
                    {
                        HealthEvent = healthEvent,
                        TypeEvent = healthEvent.IdEventType
                    };
                case TypeEventEnum.VISITAMEDICA:
                    var medicalVisitEvent = await _eventRepository.GetMedicalVisitEventById(id);
                    return new GenericEvent
                    {
                        MedicalVisitEvent = medicalVisitEvent,
                        TypeEvent = medicalVisitEvent.IdEventType
                    };
                case TypeEventEnum.ESTUDIOS:
                    var examEvent = await _eventRepository.GetExamEventById(id);
                    return new GenericEvent
                    {
                        ExamEvent = examEvent,
                        TypeEvent = examEvent.IdEventType
                    };
                case TypeEventEnum.NOTALIBRE:
                    return null;
                default:
                    return null;
            }

        }
    }
}
