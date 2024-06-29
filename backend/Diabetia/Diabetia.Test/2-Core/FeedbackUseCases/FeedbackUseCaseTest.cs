using Diabetia.Application.UseCases;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.Feedback
{
    public class FeedbackUseCaseTest
    {
        private readonly IPatientValidator _fakePatientValidator;
        private readonly IFeedbackRepository _fakeFeedbackRepository;
        private readonly IUserRepository _fakeUserRepository;
        private readonly FeedbackUseCase _feedbackUseCase;
        public FeedbackUseCaseTest()
        {
            _fakePatientValidator = A.Fake<IPatientValidator>();
            _fakeUserRepository = A.Fake<IUserRepository>();
            _fakeFeedbackRepository = A.Fake<IFeedbackRepository>();
            _feedbackUseCase = new FeedbackUseCase(_fakePatientValidator, _fakeUserRepository, _fakeFeedbackRepository);
        }

        [Fact]
        public async Task GetEventsToFeedback_GivenValidEmail_ShouldGetAllEventsSuccessfully()
        {
            //Arrange
            var email = "test@gmail.com";
            var patient = new Paciente() { Id = 1 };


            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patient);
            A.CallTo(() => _fakeFeedbackRepository.GetAllEventsWithoutFeedback(patient.Id))
            .Returns(Task.FromResult(new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Title", "Comida" },
                    { "IdEvent", 1 },
                    { "KindEventId", 1 },
                    { "EventDate", DateTime.Now },
                    { "Carbohydrates", 30 }
                }
            }));

            //Act
            var response = await _feedbackUseCase.GetEventsToFeedback(email);

            //Assert
            Assert.NotNull(response);
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeFeedbackRepository.GetAllEventsWithoutFeedback(patient.Id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetEventsToFeedback_GivenValidEmailNotFeedbackPending_ShouldGiveEmptyListSuccessfully()
        {
            //Arrange
            var email = "test@gmail.com";
            var patient = new Paciente() { Id = 1 };


            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patient);
            A.CallTo(() => _fakeFeedbackRepository.GetAllEventsWithoutFeedback(patient.Id))
            .Returns(Task.FromResult(new List<Dictionary<string, object>>()));

            //Act
            var response = await _feedbackUseCase.GetEventsToFeedback(email);

            //Assert
            Assert.NotNull(response);
            Assert.Empty(response);
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeFeedbackRepository.GetAllEventsWithoutFeedback(patient.Id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetEventsToFeedback_GivenInvalidPatient_ThrowsPatientNotFoundException()
        {
            //Arrange
            var email = "test@gmail.com";
            var patient = new Paciente() { Id = 1 };


            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            //Act & Assert
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _feedbackUseCase.GetEventsToFeedback(email));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddFeedbackAsync_GivenValidEmail_ShouldAddFeedbackSuccessfully()
        {
            // Arrange
            var email = "test@email.com";
            var feedback = new Diabetia.Domain.Models.Feedback()
            {
                Id = 1,
            };

            // Act
            await _feedbackUseCase.AddFeedbackAsync(email, feedback);
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeFeedbackRepository.AddFeedback(feedback)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddFeedbackAsync_GivenInvalidPatient_ThrowsPatientNotFoundException()
        {
            // Arrange
            var email = "test@email.com";
            var feedback = new Diabetia.Domain.Models.Feedback()
            {
                Id = 1,
            };

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            // Act & Assert
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _feedbackUseCase.AddFeedbackAsync(email, feedback));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }
    }
}
