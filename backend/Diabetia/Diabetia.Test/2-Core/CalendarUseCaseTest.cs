using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using FakeItEasy;

namespace Diabetia_Core.Events;
public class CalendarUseCaseTests
{
    private readonly IUserRepository _userRepository;
    private readonly IEventRepository _eventRepository;
    private readonly CalendarUseCase _calendarUseCase;

    public CalendarUseCaseTests()
    {
        _userRepository = A.Fake<IUserRepository>();
        _eventRepository = A.Fake<IEventRepository>();

        _calendarUseCase = new CalendarUseCase(_eventRepository, _userRepository);
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
        var userRepository = A.Fake<IUserRepository>();
        var eventRepository = A.Fake<IEventRepository>();

        var calendarUseCase = new CalendarUseCase(eventRepository, userRepository);

        var date = new DateTime(2024, 6, 7);
        var email = "test@example.com";
        var userId = 123;

        A.CallTo(() => userRepository.GetPatient(email)).Returns(new Paciente { Id = userId });

        var physicalActivityEvents = new List<PhysicalActivityEvent>
        {
            new PhysicalActivityEvent { DateEvent = date.AddDays(1), Title = "Physical Activity 1" },
            new PhysicalActivityEvent { DateEvent = date, Title = "Physical Activity 2" }
        };
        A.CallTo(() => eventRepository.GetPhysicalActivity(userId, date)).Returns(physicalActivityEvents);

        var foodEvents = new List<FoodEvent>
        {
            new FoodEvent { DateEvent = date, IngredientName = "Ingredient 1" },
            new FoodEvent { DateEvent = date.AddDays(1), IngredientName = "Ingredient 2" }
        };
        A.CallTo(() => eventRepository.GetFoods(userId, date)).Returns(foodEvents);


        // Act
        var events = await calendarUseCase.GetAllEventsByDate(date, email);

        // Assert
        Assert.NotNull(events);
        Assert.Contains(events, e => e.Title == "Comida" && e.AdditionalInfo == "Ingredientes: Ingredient 1");
        Assert.Contains(events, e => e.Title == "Physical Activity 2");
    }
}