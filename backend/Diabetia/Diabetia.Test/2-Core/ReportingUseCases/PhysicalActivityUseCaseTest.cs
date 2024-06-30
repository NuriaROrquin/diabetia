using Diabetia.Application.UseCases.ReportingUseCases;
using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;
using System.Security.AccessControl;

namespace Diabetia_Core.Reporting
{
    public class PhysicalActivityUseCaseTest
    {
        private readonly IPatientValidator _fakePatientValidator;
        private readonly IUserRepository _fakeUserRepository;
        private readonly IReportingRepository _fakeReportingRepository;
        private readonly PhysicalActivityReportUseCase _fakePhysicalActivityReportUseCase;

        public PhysicalActivityUseCaseTest()
        {
            _fakePatientValidator = A.Fake<IPatientValidator>();
            _fakeUserRepository = A.Fake<IUserRepository>();
            _fakeReportingRepository = A.Fake<IReportingRepository>();
            _fakePhysicalActivityReportUseCase = new PhysicalActivityReportUseCase(_fakePatientValidator, _fakeUserRepository, _fakeReportingRepository);
        }


        [Fact]

        public async Task GetPhysicalActivityToReporting_GivingValidData_ShouldGetPhysicalActivitySuccessfully()
        {
            var email = "test@diabetia.com";
            var dateFrom = DateTime.Now.AddDays(1);
            var dateTo = DateTime.Now.AddDays(3);
            var patient = new Paciente() { Id = 1 };
            var physicalActivities = new List<EventSummary>
            {
                new EventSummary
                {
                     EventDay = DateTime.Now.AddDays(2),
                      AmountEvents = 1
                }
            };

            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patient);
            A.CallTo(() => _fakeReportingRepository.GetPhysicalActivityEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult(physicalActivities));

            //Act
            var response = await _fakePhysicalActivityReportUseCase.GetPhysicalActivityToReporting(email, dateFrom, dateTo);

            //Assert
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeReportingRepository.GetPhysicalActivityEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();


        }

        [Fact]

        public async Task GetPhysicalActivityToReporting_GivingValidData_ShouldGetEmptyPhysicalActivitySuccessfully()
        {
            var email = "test@diabetia.com";
            var dateFrom = DateTime.Now.AddDays(1);
            var dateTo = DateTime.Now.AddDays(3);
            var patient = new Paciente() { Id = 1 };
            var physicalActivities = new List<EventSummary>();

            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patient);
            A.CallTo(() => _fakeReportingRepository.GetPhysicalActivityEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult(physicalActivities));

            //Act
            var response = await _fakePhysicalActivityReportUseCase.GetPhysicalActivityToReporting(email, dateFrom, dateTo);

            //Assert
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeReportingRepository.GetPhysicalActivityEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
            Assert.Empty(response);
        }

        [Fact]

        public async Task GetPhysicalActivityToReporting_GivingInvalidPatient_ThrowPatientNotFoundException()
        {
            var email = "test@diabetia.com";
            var dateFrom = DateTime.Now.AddDays(1);
            var dateTo = DateTime.Now.AddDays(3);

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            //ActAndAssert
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakePhysicalActivityReportUseCase.GetPhysicalActivityToReporting(email, dateFrom, dateTo));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]

        public async Task GetPhysicalActivityDurationToReporting_GivingValidData_ShouldGetPhysicalActivityDurationSuccessfully()
        {
            var email = "test@diabetia.com";
            var dateFrom = DateTime.Now.AddDays(1);
            var dateTo = DateTime.Now.AddDays(3);
            var patient = new Paciente() { Id = 1 };
            var physicalActivitiesDurations = new List<ActivityDurationSummary>
            {
                new ActivityDurationSummary
                {
                    ActivityName = "Running",
                    TotalDuration = 30
                }
             };

            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patient);
            A.CallTo(() => _fakeReportingRepository.GetPhysicalActivityEventDurationsByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult(physicalActivitiesDurations));

            // Act
            var response = await _fakePhysicalActivityReportUseCase.GetPhysicalActivityDurationToReporting(email, dateFrom, dateTo);

            // Assert
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeReportingRepository.GetPhysicalActivityEventDurationsByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();

            // Assert the response
            Assert.NotNull(response);
            Assert.Single(response); // Verifies that there is exactly one item in the response

            var actualActivity = response.First();
            Assert.Equal("Running", actualActivity.ActivityName);
            Assert.Equal(30, actualActivity.TotalDuration);

        }

        [Fact]

        public async Task GetPhysicalActivityDurationToReporting_GivingValidData_ShouldGetEmptyPhysicalActivityDurationSuccessfully()
        {
            var email = "test@diabetia.com";
            var dateFrom = DateTime.Now.AddDays(1);
            var dateTo = DateTime.Now.AddDays(3);
            var patient = new Paciente() { Id = 1 };
            var physicalActivitiesDurations = new List<ActivityDurationSummary>();

            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patient);
            A.CallTo(() => _fakeReportingRepository.GetPhysicalActivityEventDurationsByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult(physicalActivitiesDurations));

            // Act
            var response = await _fakePhysicalActivityReportUseCase.GetPhysicalActivityDurationToReporting(email, dateFrom, dateTo);

            // Assert
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeReportingRepository.GetPhysicalActivityEventDurationsByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();

            Assert.Empty(response);

        }

        [Fact]

        public async Task GetPhysicalActivityDurationToReporting_GivingInvalidPatient_ThrowPatientNotFoundException()
        {
            var email = "test@diabetia.com";
            var dateFrom = DateTime.Now.AddDays(1);
            var dateTo = DateTime.Now.AddDays(3);

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            //ActAndAssert
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakePhysicalActivityReportUseCase.GetPhysicalActivityDurationToReporting(email, dateFrom, dateTo));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }
    }
}
