using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Domain.Utilities;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.Events
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
        public async Task EventUseCaseDeleteEventInsuline_WhenCalledWithEventId_ShouldDeleteEventInsulineSuccessfully()
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
            
            A.CallTo(() => _fakeEventRepository.DeleteInsulinEventAsync(eventId)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async Task EventUseCaseDeleteEventGlucose_WhenCalledWithEventId_ShouldDeleteEventGlucoseSuccessfully()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = (int)TypeEventEnum.GLUCOSA
            };

            var eventType = TypeEventEnum.GLUCOSA;

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).Returns(@event);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(Task.FromResult(eventType));

            // Act
            await _fakeEventUseCase.DeleteEvent(eventId, email);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeEventRepository.DeleteGlucoseEventAsync(eventId)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async Task EventUseCaseDeleteEventPhysicalActivity_WhenCalledWithEventId_ShouldDeleteEventPhysicalActivitySuccessfully()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = (int)TypeEventEnum.ACTIVIDADFISICA
            };

            var eventType = TypeEventEnum.ACTIVIDADFISICA;

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).Returns(@event);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(Task.FromResult(eventType));

            // Act
            await _fakeEventUseCase.DeleteEvent(eventId, email);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            
            A.CallTo(() => _fakeEventRepository.DeletePhysicalActivityEventAsync(eventId))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async Task EventUseCaseDeleteEventFreeNote_WhenCalledWithEventId_ShouldDeleteEventFreeNoteSuccessfully()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = (int)TypeEventEnum.NOTALIBRE
            };

            var eventType = TypeEventEnum.NOTALIBRE;

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).Returns(@event);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(Task.FromResult(eventType));

            // Act
            await _fakeEventUseCase.DeleteEvent(eventId, email);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            
            
            A.CallTo(() => _fakeEventRepository.DeleteFreeNoteEventAsync(eventId))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async Task EventUseCaseDeleteEventFood_WhenCalledWithEventId_ShouldDeleteEventFoodSuccessfully()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = (int)TypeEventEnum.COMIDA
            };

            var eventType = TypeEventEnum.COMIDA;

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).Returns(@event);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(Task.FromResult(eventType));

            // Act
            await _fakeEventUseCase.DeleteEvent(eventId, email);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            
            A.CallTo(() => _fakeEventRepository.DeleteFoodEventAsync(eventId)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async Task EventUseCaseDeleteEventMedicalVisit_WhenCalledWithEventId_ShouldDeleteEventMedicalVisitSuccessfully()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = (int)TypeEventEnum.VISITAMEDICA
            };

            var eventType = TypeEventEnum.VISITAMEDICA;

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).Returns(@event);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(Task.FromResult(eventType));

            // Act
            await _fakeEventUseCase.DeleteEvent(eventId, email);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            
            
            A.CallTo(() => _fakeEventRepository.DeleteMedicalVisitEventAsync(eventId))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async Task EventUseCaseDeleteEventMedicalExam_WhenCalledWithEventId_ShouldDeleteEventMedicalExamSuccessfully()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = (int)TypeEventEnum.ESTUDIOS
            };

            var eventType = TypeEventEnum.ESTUDIOS;

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).Returns(@event);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(Task.FromResult(eventType));

            // Act
            await _fakeEventUseCase.DeleteEvent(eventId, email);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            
            A.CallTo(() => _fakeEventRepository.DeleteMedicalExaminationEvent(eventId))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeTagRecognitionProvider.DeleteFileFromBucket(A<string>.Ignored))
                .MustHaveHappenedOnceExactly(); 
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
        
        [Fact]
        public async Task GetEvent_WhenCalledWithGlucoseEvent_ShouldReturnGenericEvent()
        {
            // Arrange
            var eventId = 1;
            var eventType = TypeEventEnum.GLUCOSA;
            var glucoseEvent = new GlucoseEvent { IdEventType = (int)eventType };

            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(eventType);
            A.CallTo(() => _fakeEventRepository.GetGlucoseEventById(eventId)).Returns(glucoseEvent);

            // Act
            var result = await _fakeEventUseCase.GetEvent(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)eventType, result.TypeEvent);
            Assert.Equal(glucoseEvent, result.GlucoseEvent);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetGlucoseEventById(eventId)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public async Task GetEvent_WhenCalledWithInsulinEvent_ShouldReturnGenericEvent()
        {
            // Arrange
            var eventId = 2;
            var eventType = TypeEventEnum.INSULINA;
            var insulinEvent = new InsulinEvent { IdEventType = (int)eventType };

            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(eventType);
            A.CallTo(() => _fakeEventRepository.GetInsulinEventById(eventId)).Returns(insulinEvent);

            // Act
            var result = await _fakeEventUseCase.GetEvent(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)eventType, result.TypeEvent);
            Assert.Equal(insulinEvent, result.InsulinEvent);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetInsulinEventById(eventId)).MustHaveHappenedOnceExactly();
        }

        
        [Fact]
        public async Task GetEvent_WhenCalledWithFoodEvent_ShouldReturnGenericEvent()
        {
            // Arrange
            var eventId = 3;
            var eventType = TypeEventEnum.COMIDA;
            var foodEvent = new FoodEvent { IdEventType = (int)eventType };

            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(eventType);
            A.CallTo(() => _fakeEventRepository.GetFoodEventById(eventId)).Returns(foodEvent);

            // Act
            var result = await _fakeEventUseCase.GetEvent(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)eventType, result.TypeEvent);
            Assert.Equal(foodEvent, result.FoodEvent);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetFoodEventById(eventId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetEvent_WhenCalledWithPhysicalActivityEvent_ShouldReturnGenericEvent()
        {
            // Arrange
            var eventId = 4;
            var eventType = TypeEventEnum.ACTIVIDADFISICA;
            var physicalActivityEvent = new PhysicalActivityEvent { IdEventType = (int)eventType };

            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(eventType);
            A.CallTo(() => _fakeEventRepository.GetPhysicalActivityById(eventId)).Returns(physicalActivityEvent);

            // Act
            var result = await _fakeEventUseCase.GetEvent(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)eventType, result.TypeEvent);
            Assert.Equal(physicalActivityEvent, result.PhysicalActivityEvent);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetPhysicalActivityById(eventId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetEvent_WhenCalledWithHealthEvent_ShouldReturnGenericEvent()
        {
            // Arrange
            var eventId = 5;
            var eventType = TypeEventEnum.EVENTODESALUD;
            var healthEvent = new HealthEvent { IdEventType = (int)eventType };

            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(eventType);
            A.CallTo(() => _fakeEventRepository.GetHealthEventById(eventId)).Returns(healthEvent);

            // Act
            var result = await _fakeEventUseCase.GetEvent(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)eventType, result.TypeEvent);
            Assert.Equal(healthEvent, result.HealthEvent);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetHealthEventById(eventId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetEvent_WhenCalledWithMedicalVisitEvent_ShouldReturnGenericEvent()
        {
            // Arrange
            var eventId = 6;
            var eventType = TypeEventEnum.VISITAMEDICA;
            var medicalVisitEvent = new MedicalVisitEvent { IdEventType = (int)eventType };

            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(eventType);
            A.CallTo(() => _fakeEventRepository.GetMedicalVisitEventById(eventId)).Returns(medicalVisitEvent);

            // Act
            var result = await _fakeEventUseCase.GetEvent(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)eventType, result.TypeEvent);
            Assert.Equal(medicalVisitEvent, result.MedicalVisitEvent);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetMedicalVisitEventById(eventId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetEvent_WhenCalledWithExamEvent_ShouldReturnGenericEvent()
        {
            // Arrange
            var eventId = 7;
            var eventType = TypeEventEnum.ESTUDIOS;
            var examEvent = new ExamEvent { IdEventType = (int)eventType };

            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(eventType);
            A.CallTo(() => _fakeEventRepository.GetExamEventById(eventId)).Returns(examEvent);

            // Act
            var result = await _fakeEventUseCase.GetEvent(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)eventType, result.TypeEvent);
            Assert.Equal(examEvent, result.ExamEvent);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetExamEventById(eventId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetEvent_WhenCalledWithInvalidEventType_ShouldReturnNull()
        {
            // Arrange
            var eventId = 8;
            var eventType = (TypeEventEnum)999; // Unrecognized event type

            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(eventType);

            // Act
            var result = await _fakeEventUseCase.GetEvent(eventId);

            // Assert
            Assert.Null(result);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).MustHaveHappenedOnceExactly();
        }

        
        [Fact]
        public async Task GetEvent_WhenCalledWithFreeNoteEvent_ShouldReturnNull()
        {
            // Arrange
            var eventId = 9;
            var eventType = TypeEventEnum.NOTALIBRE;

            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns(eventType);

            // Act
            var result = await _fakeEventUseCase.GetEvent(eventId);

            // Assert
            Assert.Null(result);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteEvent_WhenCalledWithUnknownEventType_ShouldNotCallAnyDeleteMethods()
        {
            // Arrange
            var email = "fakeEmail@gmail.com";
            var eventId = 1;
            var @event = new CargaEvento()
            {
                IdTipoEvento = 999 
            };

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).Returns(@event);
            A.CallTo(() => _fakeEventRepository.GetEventType(eventId)).Returns((TypeEventEnum)999);

            // Act
            await _fakeEventUseCase.DeleteEvent(eventId, email);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(eventId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            
            A.CallTo(() => _fakeEventRepository.DeleteInsulinEventAsync(eventId)).MustNotHaveHappened();
            A.CallTo(() => _fakeEventRepository.DeleteGlucoseEventAsync(eventId)).MustNotHaveHappened();
            A.CallTo(() => _fakeEventRepository.DeletePhysicalActivityEventAsync(eventId)).MustNotHaveHappened();
            A.CallTo(() => _fakeEventRepository.DeleteFreeNoteEventAsync(eventId)).MustNotHaveHappened();
            A.CallTo(() => _fakeEventRepository.DeleteFoodEven(eventId)).MustNotHaveHappened();
            A.CallTo(() => _fakeEventRepository.DeleteMedicalVisitEventAsync(eventId)).MustNotHaveHappened();
            A.CallTo(() => _fakeEventRepository.DeleteMedicalExaminationEvent(eventId)).MustNotHaveHappened();
            A.CallTo(() => _fakeTagRecognitionProvider.DeleteFileFromBucket(A<string>.Ignored)).MustNotHaveHappened();
        }


    }
}
