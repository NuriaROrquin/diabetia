using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Domain.Utilities;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia.Test._2_Core.EventUseCases
{
    public class EventUseCaseTest
    {
        private readonly IEventRepository _fakeEventRepository;
        private readonly ITagRecognitionProvider _fakeTagRecognitionProvider;
        private readonly IPatientValidator _fakePatientValidator;
        private readonly IPatientEventValidator _fakePatientEventValidator;
        private readonly EventUseCase _fakeEventUseCase;
        public EventUseCaseTest() 
        {
            _fakeEventRepository = A.Fake<IEventRepository>();
            _fakePatientValidator = A.Fake<IPatientValidator>();
            _fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            _fakeTagRecognitionProvider = A.Fake<ITagRecognitionProvider>();
            _fakeEventUseCase = new EventUseCase(_fakeEventRepository, _fakeTagRecognitionProvider, _fakePatientValidator, _fakePatientEventValidator);
        }

        [Fact]
        public async Task EventUseCaseDeleteEvent_WhenCalledWithEventId_ShouldDeleteEventSuccessfully()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = (int)TypeEventEnum.INSULINA
            };

            var eventType = TypeEventEnum.INSULINA;

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).Returns(@event);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(Task.FromResult(eventType));

            // Act
            await _fakeEventUseCase.DeleteEvent(eventId, email);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            
            switch (eventType)
            {
                case TypeEventEnum.INSULINA:
                    A.CallTo(() => _fakeEventRepository.DeleteInsulinEvent(eventId)).MustHaveHappenedOnceExactly();
                    break;
                case TypeEventEnum.GLUCOSA:
                    A.CallTo(() => _fakeEventRepository.DeleteGlucoseEventAsync(eventId)).MustHaveHappenedOnceExactly();
                    break;
                case TypeEventEnum.ACTIVIDADFISICA:
                    A.CallTo(() => _fakeEventRepository.DeletePhysicalActivityEventAsync(eventId)).MustHaveHappenedOnceExactly();
                    break;
                case TypeEventEnum.NOTALIBRE:
                    A.CallTo(() => _fakeEventRepository.DeleteFreeNoteEventAsync(eventId)).MustHaveHappenedOnceExactly();
                    break;
                case TypeEventEnum.COMIDA:
                    A.CallTo(() => _fakeEventRepository.DeleteFoodEven(eventId)).MustHaveHappenedOnceExactly();
                    break;
                case TypeEventEnum.VISITAMEDICA:
                    A.CallTo(() => _fakeEventRepository.DeleteMedicalVisitEventAsync(eventId)).MustHaveHappenedOnceExactly();
                    break;
                case TypeEventEnum.ESTUDIOS:
                    A.CallTo(() => _fakeEventRepository.DeleteMedicalExaminationEvent(eventId)).MustHaveHappenedOnceExactly();
                    A.CallTo(() => _fakeTagRecognitionProvider.DeleteFileFromBucket(A<string>.Ignored)).MustHaveHappenedOnceExactly();
                    break;
                default:
                    throw new InvalidOperationException("Unexpected event type.");
            }
        }

        [Fact]
        public async Task EventUseCaseDeleteEvent_GivenInvalidPatient_ThrowsPatientNotFoundException()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = (int)TypeEventEnum.INSULINA
            };

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            // Act & Assert
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakeEventUseCase.DeleteEvent(eventId, email));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EventUseCaseDeleteEvent_GivenInvalidEvent_ThrowsEventNotRelatedWithPatientException()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = (int)TypeEventEnum.INSULINA
            };

            var eventType = TypeEventEnum.INSULINA;

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).Returns(@event);
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).Throws<EventNotRelatedWithPatientException>();

            // Act & Assert
            await Assert.ThrowsAsync<EventNotRelatedWithPatientException>(() => _fakeEventUseCase.DeleteEvent(eventId, email));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
        }
    }
}
