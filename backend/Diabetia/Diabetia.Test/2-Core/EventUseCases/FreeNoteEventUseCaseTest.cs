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
        private readonly FreeNoteUseCase _fakeFreeNoteUseCase;

        public FreeNoteEventUseCaseTest()
        {
            _fakeEventRepository = A.Fake<IEventRepository>();
            _fakePatientValidator = A.Fake<IPatientValidator>();
            //_fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            _fakeUserRepository = A.Fake<IUserRepository>();
            _fakeFreeNoteUseCase = new FreeNoteUseCase(_fakeEventRepository, _fakePatientValidator, _fakeUserRepository);
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
    }
}
