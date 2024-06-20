using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia.Test._2_Core.EventUseCases
{
    public class FreeNoteEventUseCaseTest
    {
        private readonly IEventRepository _fakeEventRepository;
        private readonly IPatientValidator _fakePatientValidator;
        private readonly IUserRepository _fakeUserRepository;
        private readonly IPatientEventValidator _fakePatientEventValidator;
        private readonly FreeNoteUseCase _fakeFreeNoteUseCase;

        public FreeNoteEventUseCaseTest()
        {
            _fakeEventRepository = A.Fake<IEventRepository>();
            _fakePatientValidator = A.Fake<IPatientValidator>();
            _fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            _fakeUserRepository = A.Fake<IUserRepository>();
            _fakeFreeNoteUseCase = new FreeNoteUseCase(_fakeEventRepository, _fakePatientValidator, _fakeUserRepository, _fakePatientEventValidator);
        }

        // --------------------------------------- ⬇⬇ Add FreeNote Event ⬇⬇ ---------------------------------------
        [Fact]
        public async Task AddFreeNoteEventUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            // Arrange
            var email = "emailTest@example.com";
            var freeNoteEvent = new CargaEvento();
            var patient = new Paciente()
            {
                Id = 1
            };

            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patient);
            await _fakeFreeNoteUseCase.AddFreeNoteEventAsync(email, freeNoteEvent);

            // Act & Assert 
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.AddFreeNoteEventAsync(patient.Id, freeNoteEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddFreeNoteEventUseCase_WhenCalledWithInvalidPatient_ThrowsPatientNotFoundException()
        {
            // Arrange
            var email = "emailTest@example.com";
            var freeNoteEvent = new CargaEvento();
            var patient = new Paciente()
            {
                Id = 1
            };
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            // Act & Assert
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakeFreeNoteUseCase.AddFreeNoteEventAsync(email, freeNoteEvent));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        // --------------------------------------- ⬇⬇ Edit FreeNote Event ⬇⬇ ---------------------------------------

        [Fact]
        public async Task EditPhysicalActivityUseCase_WhenCalledWithValidData_ShouldUpdateEventSuccessfully()
        {
            // Assert
            var email = "emailTest@example.com";
            var freeNoteEvent = new CargaEvento()
            {
                Id = 1,
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = true
            };

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(freeNoteEvent.Id)).Returns(freeNoteEvent);

            // Act
            await _fakeFreeNoteUseCase.EditFreeNoteEventAsync(email, freeNoteEvent);

            // Assert 
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(freeNoteEvent.Id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, freeNoteEvent)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.EditFreeNoteEventAsync(freeNoteEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditPhysicalActivityUseCase_WhenCalledWithInvalidPatient_ThrowsPatientNotFoundException()
        {
            // Assert
            var email = "emailTest@example.com";
            var freeNoteEvent = new CargaEvento()
            {
                Id = 1,
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = true
            };

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            // Act & Assert 
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakeFreeNoteUseCase.EditFreeNoteEventAsync(email, freeNoteEvent));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditPhysicalActivityUseCase_WhenCalledWithInvalidEvent_ThrowsEventNotRelatedWithPatientException()
        {
            // Assert
            var email = "emailTest@example.com";
            var freeNoteEvent = new CargaEvento()
            {
                Id = 1,
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = true
            };

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(freeNoteEvent.Id)).Returns(freeNoteEvent);
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, freeNoteEvent)).Throws<EventNotRelatedWithPatientException>();

            // Act & Assert 
            await Assert.ThrowsAsync<EventNotRelatedWithPatientException>(() => _fakeFreeNoteUseCase.EditFreeNoteEventAsync(email, freeNoteEvent));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(freeNoteEvent.Id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, freeNoteEvent)).MustHaveHappenedOnceExactly();
        }
    }
}
