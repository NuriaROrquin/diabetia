using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.Calendar;
public class CalendarUseCaseTests
{
    private readonly IUserRepository _userRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IPatientValidator _patientValidator;
    private readonly CalendarUseCase _calendarUseCase;

    public CalendarUseCaseTests()
    {
        _userRepository = A.Fake<IUserRepository>();
        _eventRepository = A.Fake<IEventRepository>();
        _patientValidator = A.Fake<IPatientValidator>();

        _calendarUseCase = new CalendarUseCase(_eventRepository, _userRepository, _patientValidator);
    }

    [Fact]
    public async Task GetAllEvents_WhenDateIsNull_ReturnsCorrectEvents()
    {
        var email = "test@gmail.com";
        int patientId = 40;

        var patient = new Paciente { Id = patientId };
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));

        var physicalActivityEvents = new List<PhysicalActivityEvent>
        {
            new PhysicalActivityEvent { DateEvent = new DateTime(2024, 6, 2, 8, 0, 0), Title = "Morning Run" }
        };
        A.CallTo(() => _eventRepository.GetPhysicalActivity(patientId, null)).Returns(Task.FromResult((IEnumerable<PhysicalActivityEvent>)physicalActivityEvents));

        var foodEvents = new List<FoodEvent>
        {
            new FoodEvent { DateEvent = new DateTime(2024, 6, 1, 12, 0, 0), IngredientName = "Apple" }
        };
        A.CallTo(() => _eventRepository.GetFoods(patientId, null)).Returns(Task.FromResult((IEnumerable<FoodEvent>)foodEvents));

        var examEvents = new List<ExamEvent>
        {
            new ExamEvent { DateEvent = new DateTime(2024, 6, 1, 9, 0, 0), Title = "Blood Test" }
        };
        A.CallTo(() => _eventRepository.GetExams(patientId, null)).Returns(Task.FromResult((IEnumerable<ExamEvent>)examEvents));

        var glucoseEvents = new List<GlucoseEvent>
        {
            new GlucoseEvent { DateEvent = new DateTime(2024, 6, 1, 7, 0, 0), Title = "Morning Glucose", GlucoseLevel = 90 }
        };
        A.CallTo(() => _eventRepository.GetGlycemia(patientId, null)).Returns(Task.FromResult((IEnumerable<GlucoseEvent>)glucoseEvents));

        var insulinEvents = new List<InsulinEvent>
        {
            new InsulinEvent { DateEvent = new DateTime(2024, 6, 1, 7, 30, 0), Title = "Morning Insulin", Dosage = 10, InsulinType = "Rapid" }
        };
        A.CallTo(() => _eventRepository.GetInsulin(patientId, null)).Returns(Task.FromResult((IEnumerable<InsulinEvent>)insulinEvents));

        var healthEvents = new List<HealthEvent>
        {
            new HealthEvent { DateEvent = new DateTime(2024, 6, 1, 10, 0, 0), Title = "Check-up" }
        };
        A.CallTo(() => _eventRepository.GetHealth(patientId, null)).Returns(Task.FromResult((IEnumerable<HealthEvent>)healthEvents));

        var medicalVisitEvents = new List<MedicalVisitEvent>
        {
            new MedicalVisitEvent { DateEvent = new DateTime(2024, 6, 1, 11, 0, 0), Title = "Doctor Appointment", Description = "Routine check" }
        };
        A.CallTo(() => _eventRepository.GetMedicalVisit(patientId, null)).Returns(Task.FromResult((IEnumerable<MedicalVisitEvent>)medicalVisitEvents));

        // Act
        var result = await _calendarUseCase.GetAllEvents(email);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.ContainsKey("2024-06-01"));
        Assert.Equal(6, result["2024-06-01"].Count);
    }

    [Fact]
    public async Task GetAllEventsByDate_WhenDateExists_ReturnsCorrectEvents()
    {
        // Arrange
        var email = "test@example.com";
        var date = new DateTime(2024, 6, 7);
        int patientId = 123;

        var patient = new Paciente { Id = patientId };
        A.CallTo(() => _userRepository.GetPatient(email)).Returns(Task.FromResult(patient));

        var physicalActivityEvents = new List<PhysicalActivityEvent>
        {
            new PhysicalActivityEvent { IdEvent = 1, DateEvent = date, Title = "Morning Run", Duration = 30 }
        };
        A.CallTo(() => _eventRepository.GetPhysicalActivity(patientId, date)).Returns(Task.FromResult((IEnumerable<PhysicalActivityEvent>)physicalActivityEvents));

        var foodEvents = new List<FoodEvent>
        {
            new FoodEvent { IdEvent = 2, DateEvent = date, IngredientName = "Apple" }
        };
        A.CallTo(() => _eventRepository.GetFoods(patientId, date)).Returns(Task.FromResult((IEnumerable<FoodEvent>)foodEvents));

        var examEvents = new List<ExamEvent>
        {
            new ExamEvent { IdEvent = 3, DateEvent = date, Title = "Blood Test" }
        };
        A.CallTo(() => _eventRepository.GetExams(patientId, date)).Returns(Task.FromResult((IEnumerable<ExamEvent>)examEvents));

        var glucoseEvents = new List<GlucoseEvent>
        {
            new GlucoseEvent { IdEvent = 4, DateEvent = date, Title = "Morning Glucose", GlucoseLevel = 90 }
        };
        A.CallTo(() => _eventRepository.GetGlycemia(patientId, date)).Returns(Task.FromResult((IEnumerable<GlucoseEvent>)glucoseEvents));

        var insulinEvents = new List<InsulinEvent>
        {
            new InsulinEvent { IdEvent = 5, DateEvent = date, Title = "Morning Insulin" }
        };
        A.CallTo(() => _eventRepository.GetInsulin(patientId, date)).Returns(Task.FromResult((IEnumerable<InsulinEvent>)insulinEvents));

        var healthEvents = new List<HealthEvent>
        {
            new HealthEvent { IdEvent = 6, DateEvent = date, Title = "Check-up" }
        };
        A.CallTo(() => _eventRepository.GetHealth(patientId, date)).Returns(Task.FromResult((IEnumerable<HealthEvent>)healthEvents));

        var medicalVisitEvents = new List<MedicalVisitEvent>
        {
            new MedicalVisitEvent { IdEvent = 7, DateEvent = date, Title = "Doctor Appointment", Description = "Routine check" }
        };
        A.CallTo(() => _eventRepository.GetMedicalVisit(patientId, date)).Returns(Task.FromResult((IEnumerable<MedicalVisitEvent>)medicalVisitEvents));

        // Act
        var events = await _calendarUseCase.GetAllEventsByDate(date, email);

        // Assert
        Assert.NotNull(events);

        Assert.Contains(events, e => e.Title == "Morning Run" && e.AdditionalInfo == "Duración: 30min");
        Assert.Contains(events, e => e.Title == "Comida" && e.AdditionalInfo == "Ingredientes: Apple");
        Assert.Contains(events, e => e.Title == "Blood Test");
        Assert.Contains(events, e => e.Title == "Morning Glucose" && e.AdditionalInfo == "Nivel: 90mg/dL");
        Assert.Contains(events, e => e.Title == "Morning Insulin");
        Assert.Contains(events, e => e.Title == "Check-up");
        Assert.Contains(events, e => e.Title == "Doctor Appointment" && e.AdditionalInfo == "Routine check");
    }
}