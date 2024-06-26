﻿using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Common.Utilities;
using Diabetia.Domain.Repositories;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using Diabetia.Domain.Entities.Events;
using Microsoft.Extensions.Logging;

namespace Diabetia.Application.UseCases
{
    public class EventUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITagRecognitionProvider _tagRecognitionProvider;
        private object glucoseEvent;

        public EventUseCase(IEventRepository eventRepository, ITagRecognitionProvider tagRecognitionProvider)
        {
            _eventRepository = eventRepository;
            _tagRecognitionProvider = tagRecognitionProvider;
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

       public async Task DeleteEvent(int id)
        {
            var type = await _eventRepository.GetEventType(id);

            switch (type)
            {
                case TypeEventEnum.INSULINA:
                    await _eventRepository.DeleteInsulinEvent(id);
                    break;
                case TypeEventEnum.GLUCOSA:
                    await _eventRepository.DeleteGlucoseEvent(id);
                    break;
                case TypeEventEnum.ACTIVIDADFISICA:
                    await _eventRepository.DeletePhysicalActivityEventAsync(id);
                    break;
                case TypeEventEnum.NOTALIBRE:
                    break;
                case TypeEventEnum.COMIDA:
                    await _eventRepository.DeleteFoodEven(id);
                    break;
                case TypeEventEnum.VISITAMEDICA:
                    await _eventRepository.DeleteMedicalVisitEventAsync(id);
                    break;
                case TypeEventEnum.ESTUDIOS:
                    string idOnBucket = await _eventRepository.DeleteMedicalExaminationEvent(id);
                    await _tagRecognitionProvider.DeleteFileFromBucket(idOnBucket);
                    break;
                default:
                    break;
            }

        }
    }
}
