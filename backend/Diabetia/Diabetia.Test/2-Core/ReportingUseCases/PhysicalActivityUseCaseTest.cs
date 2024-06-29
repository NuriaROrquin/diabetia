using Diabetia.Application.UseCases.ReportingUseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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

            //Act
            var response = _fakePhysicalActivityReportUseCase.GetPhysicalActivityToReporting(email, dateFrom, dateTo);

            //Assert
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeReportingRepository.GetPhysicalActivityEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();

        }
    }
}
