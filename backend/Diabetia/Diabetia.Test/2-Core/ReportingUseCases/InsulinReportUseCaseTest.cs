using Diabetia.Application.UseCases.ReportingUseCases;
using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.Reporting;

public class InsulinReportUseCaseTest
{
    private readonly IPatientValidator _patientValidator;
    private readonly IUserRepository _userRepository;
    private readonly IReportingRepository _reportingRepository;
    private readonly InsulinReportUseCase _useCase;

    public InsulinReportUseCaseTest()
    {
        _patientValidator = A.Fake<IPatientValidator>();
        _userRepository = A.Fake<IUserRepository>();
        _reportingRepository = A.Fake<IReportingRepository>();
        _useCase = new InsulinReportUseCase(_patientValidator, _userRepository, _reportingRepository);
    }

    [Fact]
    public async Task GetInsulinToReporting_ShouldReturnEmptyList_WhenNoInsulinEvents()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente() { Id = 1 };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetInsulinEventsToReportByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult<List<EventoInsulina>>(null));

        // Act
        var result = await _useCase.GetInsulinToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetInsulinEventsToReportByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetInsulinToReporting_ShouldReturnInsulinEventList()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente { Id = 1 };
        var insulinEvents = new List<EventoInsulina>
        {
            new EventoInsulina { /* Properties */ },
            new EventoInsulina { /* Properties */ }
        };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetInsulinEventsToReportByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult(insulinEvents));

        // Act
        var result = await _useCase.GetInsulinToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(insulinEvents.Count, result.Count);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetInsulinEventsToReportByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetInsulinSummaryEventToReporting_ShouldReturnEmptyList_WhenNoInsulinSummaryEvents()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente { Id = 1 };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetInsulinEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult<List<EventSummary>>(null));

        // Act
        var result = await _useCase.GetInsulinSummaryEventToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetInsulinEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetInsulinSummaryEventToReporting_ShouldReturnInsulinSummaryEventList()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente { Id = 1 };
        var insulinSummaryEvents = new List<EventSummary>
        {
            new EventSummary { /* Properties */ },
            new EventSummary { /* Properties */ }
        };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetInsulinEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult(insulinSummaryEvents));

        // Act
        var result = await _useCase.GetInsulinSummaryEventToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(insulinSummaryEvents.Count, result.Count);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetInsulinEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }
}