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
        private readonly IEventRepository _fakeEventRepository;
        private readonly IPatientValidator _fakePatientValidator;
        private readonly IPatientEventValidator _fakePatientEventValidator;
        private readonly IUserRepository _fakeUserRepository;
        private readonly PhysicalActivityUseCase _fakePhysicalActivityUseCase;

        public PhysicalEventUseCaseTest()
        {
            _fakeEventRepository = A.Fake<IEventRepository>();
            _fakePatientValidator = A.Fake<IPatientValidator>();
            _fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            _fakeUserRepository = A.Fake<IUserRepository>();
            _fakePhysicalActivityUseCase = new PhysicalActivityUseCase(_fakeEventRepository, _fakePatientValidator, _fakePatientEventValidator, _fakeUserRepository);
        }
        [Fact]
        public async Task AddEventPhysicalActivityUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var physicalActivityEvent = new EventoActividadFisica();
            var patient = new Paciente()
            {
                Id = 1
            };

            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patient);
            await _fakePhysicalActivityUseCase.AddPhysicalEventAsync(email, physicalActivityEvent);

            // Act & Assert 
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.AddPhysicalActivityEventAsync(patient.Id, physicalActivityEvent)).MustHaveHappenedOnceExactly();
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

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();


            // Act & Assert 
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakePhysicalActivityUseCase.AddPhysicalEventAsync(email, physicalActivityEvent));

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
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

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(physicalActivityEvent.IdCargaEventoNavigation.Id)).Returns(@event);

            // Act
            await _fakePhysicalActivityUseCase.EditPhysicalEventAsync(email, physicalActivityEvent);

            // Assert 
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.EditPhysicalActivityEventAsync(physicalActivityEvent)).MustHaveHappenedOnceExactly();
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

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            // Act & Assert 
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakePhysicalActivityUseCase.EditPhysicalEventAsync(email, physicalActivityEvent));

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
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

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(physicalActivityEvent.IdCargaEventoNavigation.Id)).Returns(@event);
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).Throws<EventNotRelatedWithPatientException>();

            // Act & Assert 
            await Assert.ThrowsAsync<EventNotRelatedWithPatientException>(() => _fakePhysicalActivityUseCase.EditPhysicalEventAsync(email, physicalActivityEvent));

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(physicalActivityEvent.IdCargaEventoNavigation.Id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
        }
    }
}
