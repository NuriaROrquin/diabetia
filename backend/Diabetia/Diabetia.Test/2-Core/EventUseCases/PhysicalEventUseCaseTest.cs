using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.Events
{
    public class PhysicalEventUseCaseTest
    {
        [Fact]
        public async Task AddEventPhysicalActivityUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var physicalActivityEvent = new EventoActividadFisica();
            var patient = new Paciente()
            {
                Id = 1
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeEventPhysicalActivityUseCase = new PhysicalActivityUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakeUserRepository.GetPatient(email)).Returns(patient);
            await fakeEventPhysicalActivityUseCase.AddPhysicalEventAsync(email, physicalActivityEvent);

            // Act & Assert 
            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.AddPhysicalActivityEventAsync(patient.Id, physicalActivityEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddEventPhysicalActivityUseCase_WhenCalledInvalidPatient_ThrowsPatientNotFoundException()
        {
            var email = "emailTest@example.com";
            var physicalActivityEvent = new EventoActividadFisica();
            var patient = new Paciente()
            {
                Id = 1
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeEventPhysicalActivityUseCase = new PhysicalActivityUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();


            // Act & Assert 
            await Assert.ThrowsAsync<PatientNotFoundException>(() => fakeEventPhysicalActivityUseCase.AddPhysicalEventAsync(email, physicalActivityEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditPhysicalActivityUseCase_WhenCalledWithValidData_ShouldUpdateEventSuccessfully()
        {
            // Assert
            var email = "emailTest@example.com";
            var physicalActivityEvent = new EventoActividadFisica()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 1,
                }
            };

            var @event = new CargaEvento()
            {
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = false
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            A.CallTo(() => fakeEventRepository.GetEventByIdAsync(physicalActivityEvent.IdCargaEventoNavigation.Id)).Returns(@event);

            var fakeEventPhysicalActivityUseCase = new PhysicalActivityUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            // Act
            await fakeEventPhysicalActivityUseCase.EditPhysicalEventAsync(email, physicalActivityEvent);

            // Assert 
            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.EditPhysicalActivityEventAsync(physicalActivityEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditPhysicalActivityUseCase_WhenCalledInvalidPatient_ThrowsPatientNotFoundException()
        {
            // Assert
            var email = "emailTest@example.com";
            var physicalActivityEvent = new EventoActividadFisica()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 1,
                }
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeEventPhysicalActivityUseCase = new PhysicalActivityUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            // Act & Assert 
            await Assert.ThrowsAsync<PatientNotFoundException>(() => fakeEventPhysicalActivityUseCase.EditPhysicalEventAsync(email, physicalActivityEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditPhysicalActivityUseCase_WhenCalledValidPatientInvalidEvent_ThrowsEventNotRelatedWithPatientException()
        {
            // Assert
            var email = "emailTest@example.com";
            var physicalActivityEvent = new EventoActividadFisica()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 1,
                }
            };

            var @event = new CargaEvento()
            {
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = false
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeEventPhysicalActivityUseCase = new PhysicalActivityUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakeEventRepository.GetEventByIdAsync(physicalActivityEvent.IdCargaEventoNavigation.Id)).Returns(@event);
            A.CallTo(() => fakePatientEventValidator.ValidatePatientEvent(email, @event)).Throws<EventNotRelatedWithPatientException>();

            // Act & Assert 
            await Assert.ThrowsAsync<EventNotRelatedWithPatientException>(() => fakeEventPhysicalActivityUseCase.EditPhysicalEventAsync(email, physicalActivityEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.GetEventByIdAsync(physicalActivityEvent.IdCargaEventoNavigation.Id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
        }
    }
}
