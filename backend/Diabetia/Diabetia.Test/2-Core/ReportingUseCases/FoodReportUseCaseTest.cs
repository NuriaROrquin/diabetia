using Diabetia.Application.UseCases.ReportingUseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.Reporting;
public class FoodReportUseCaseTests
{
    private readonly IPatientValidator _patientValidator;
    private readonly IUserRepository _userRepository;
    private readonly IReportingRepository _reportingRepository;
    private readonly FoodReportUseCase _useCase;

    public FoodReportUseCaseTests()
    {
        _patientValidator = A.Fake<IPatientValidator>();
        _userRepository = A.Fake<IUserRepository>();
        _reportingRepository = A.Fake<IReportingRepository>();
        _useCase = new FoodReportUseCase(_patientValidator, _userRepository, _reportingRepository);
    }

    [Fact]
    public async Task GetFoodToReporting_ShouldReturnEmptyList_WhenNoFoodEvents()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente() { Id = 1 };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetFoodEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult<List<EventSummary>>(null));

        // Act
        var result = await _useCase.GetFoodToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetFoodEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetFoodToReporting_ShouldReturnFoodEventSummaryList()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente() { Id = 1 };
        var foodEventSummaries = new List<EventSummary>
        {
            new EventSummary { /* Properties */ },
            new EventSummary { /* Properties */ }
        };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetFoodEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult(foodEventSummaries));

        // Act
        var result = await _useCase.GetFoodToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(foodEventSummaries.Count, result.Count);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetFoodEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }
}
