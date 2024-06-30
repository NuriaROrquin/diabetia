using Diabetia.Domain.Models;

namespace Diabetia_Core.Reporting;
using Diabetia.Application.UseCases.ReportingUseCases;
using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Domain.Utilities;
using Diabetia.Interfaces;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class GlucoseReportUseCaseTest
{
    private readonly IPatientValidator _patientValidator;
    private readonly IUserRepository _userRepository;
    private readonly IReportingRepository _reportingRepository;
    private readonly GlucoseReportUseCase _useCase;

    public GlucoseReportUseCaseTest()
    {
        _patientValidator = A.Fake<IPatientValidator>();
        _userRepository = A.Fake<IUserRepository>();
        _reportingRepository = A.Fake<IReportingRepository>();
        _useCase = new GlucoseReportUseCase(_patientValidator, _userRepository, _reportingRepository);
    }

    [Fact]
    public async Task GetGlucoseToReporting_ShouldReturnEmptyList_WhenNoGlucoseMeasures()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente() { Id = 1 };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetGlucoseEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult<List<EventSummary>>(null));

        // Act
        var result = await _useCase.GetGlucoseToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetGlucoseEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetGlucoseToReporting_ShouldReturnGlucoseEventSummaryList()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente() { Id = 1 };
        var glucoseEventSummaries = new List<EventSummary>
        {
            new EventSummary { /* Properties */ },
            new EventSummary { /* Properties */ }
        };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetGlucoseEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult(glucoseEventSummaries));

        // Act
        var result = await _useCase.GetGlucoseToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(glucoseEventSummaries.Count, result.Count);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetGlucoseEventSummaryByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetHyperglycemiaGlucoseToReporting_ShouldReturnEmptyList_WhenNoHyperglycemiaMeasures()
    {
        // Arrange
        var email = "test@example.com";
        var patient = new Paciente() { Id = 1 };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetHyperglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPERGLUCEMIA)).Returns(Task.FromResult<List<GlucoseMeasurement>>(null));

        // Act
        var result = await _useCase.GetHyperglycemiaGlucoseToReporting(email);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetHyperglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPERGLUCEMIA)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetHyperglycemiaGlucoseToReporting_ShouldReturnHyperglycemiaMeasuresList()
    {
        // Arrange
        var email = "test@example.com";
        var patient = new Paciente() { Id = 1 };
        var hyperglycemiaMeasures = new List<GlucoseMeasurement>
        {
            new GlucoseMeasurement { /* Properties */ },
            new GlucoseMeasurement { /* Properties */ }
        };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetHyperglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPERGLUCEMIA)).Returns(Task.FromResult(hyperglycemiaMeasures));

        // Act
        var result = await _useCase.GetHyperglycemiaGlucoseToReporting(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(hyperglycemiaMeasures.Count, result.Count);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetHyperglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPERGLUCEMIA)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetHypoglycemiaGlucoseToReporting_ShouldReturnEmptyList_WhenNoHypoglycemiaMeasures()
    {
        // Arrange
        var email = "test@example.com";
        var patient = new Paciente() { Id = 1 };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetHypoglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPOGLUCEMIA)).Returns(Task.FromResult<List<GlucoseMeasurement>>(null));

        // Act
        var result = await _useCase.GetHypoglycemiaGlucoseToReporting(email);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetHypoglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPOGLUCEMIA)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetHypoglycemiaGlucoseToReporting_ShouldReturnHypoglycemiaMeasuresList()
    {
        // Arrange
        var email = "test@example.com";
        var patient = new Paciente() { Id = 1 };
        var hypoglycemiaMeasures = new List<GlucoseMeasurement>
        {
            new GlucoseMeasurement { /* Properties */ },
            new GlucoseMeasurement { /* Properties */ }
        };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetHypoglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPOGLUCEMIA)).Returns(Task.FromResult(hypoglycemiaMeasures));

        // Act
        var result = await _useCase.GetHypoglycemiaGlucoseToReporting(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(hypoglycemiaMeasures.Count, result.Count);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetHypoglycemiaGlucoseHistoryByPatientId(patient.Id, GlucoseEnum.HIPOGLUCEMIA)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetGlucoseMeasurementsEventsToReporting_ShouldReturnEmptyList_WhenNoGlucoseMeasurementEvents()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente() { Id = 1 };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetGlucoseEventsToReportByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult<List<GlucoseMeasurement>>(null));

        // Act
        var result = await _useCase.GetGlucoseMeasurementsEventsToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetGlucoseEventsToReportByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetGlucoseMeasurementsEventsToReporting_ShouldReturnGlucoseMeasurementEventsList()
    {
        // Arrange
        var email = "test@example.com";
        var dateFrom = DateTime.Now.AddDays(-7);
        var dateTo = DateTime.Now;
        var patient = new Paciente() { Id = 1 };
        var glucoseMeasurementEvents = new List<GlucoseMeasurement>
        {
            new GlucoseMeasurement { /* Properties */ },
            new GlucoseMeasurement { /* Properties */ }
        };

        A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _reportingRepository.GetGlucoseEventsToReportByPatientId(patient.Id, dateFrom, dateTo)).Returns(Task.FromResult(glucoseMeasurementEvents));

        // Act
        var result = await _useCase.GetGlucoseMeasurementsEventsToReporting(email, dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(glucoseMeasurementEvents.Count, result.Count);
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _reportingRepository.GetGlucoseEventsToReportByPatientId(patient.Id, dateFrom, dateTo)).MustHaveHappenedOnceExactly();
    }
}