using System.Threading.Tasks;
using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using Xunit;
using FakeItEasy;
using Diabetia.Domain.Exceptions;

namespace Diabetia_Core.EventUseCases;
public class FoodServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IPatientValidator _patientValidator;
    private readonly IPatientEventValidator _patientEventValidator;
    private readonly FoodManuallyUseCase _foodService;

    public FoodServiceTests()
    {
        _userRepository = A.Fake<IUserRepository>();
        _eventRepository = A.Fake<IEventRepository>();
        _patientValidator = A.Fake<IPatientValidator>();
        _patientEventValidator = A.Fake<IPatientEventValidator>();
        _foodService = new FoodManuallyUseCase(_eventRepository, _patientValidator, _userRepository, _patientEventValidator);
    }

    [Fact]
    public async Task AddFoodManuallyEventAsync_ShouldReturnCorrectChConsumed()
    {
        // Arrange
        var email = "test@example.com";
        var foodEvent = new EventoComidum();
        var patient = new Patient() { Id = 1, ChCorrection = 10 };
        var chConsumed = 30;

        A.CallTo(() => _userRepository.GetPatientInfo(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _eventRepository.AddFoodEventAsync(patient.Id, foodEvent)).Returns(Task.FromResult((float)chConsumed));

        // Act
        var result = await _foodService.AddFoodManuallyEventAsync(email, foodEvent);

        // Assert
        Assert.Equal(chConsumed, result.ChConsumed);
    }

    [Fact]
    public async Task AddFoodManuallyEventAsync_ShouldReturnCorrectInsulinRecomended()
    {
        // Arrange
        var email = "test@example.com";
        var foodEvent = new EventoComidum();
        var patient = new Patient { Id = 1, ChCorrection = 10 };
        var chConsumed = 30;

        A.CallTo(() => _userRepository.GetPatientInfo(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _eventRepository.AddFoodEventAsync(patient.Id, foodEvent)).Returns(Task.FromResult((float)chConsumed));

        // Act
        var result = await _foodService.AddFoodManuallyEventAsync(email, foodEvent);

        // Assert
        Assert.Equal((float)chConsumed / patient.ChCorrection, result.InsulinRecomended);
    }

    [Fact]
    public async Task AddFoodManuallyEventAsync_ShouldCallValidatePatient()
    {
        // Arrange
        var email = "test@example.com";
        var foodEvent = new EventoComidum();
        var patient = new Patient { Id = 1, ChCorrection = 10 };
        var chConsumed = 30;

        A.CallTo(() => _userRepository.GetPatientInfo(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _eventRepository.AddFoodEventAsync(patient.Id, foodEvent)).Returns(Task.FromResult((float)chConsumed));

        // Act
        await _foodService.AddFoodManuallyEventAsync(email, foodEvent);

        // Assert
        A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappened();
    }

    [Fact]
    public async Task EditFoodManuallyEventAsync_ShouldReturnCorrectChConsumedAndInsulinRecommended()
    {
        var email = "test@example.com";
        var foodEvent = new EventoComidum
        {
            IdCargaEventoNavigation = new CargaEvento { Id = 1 }
        };
        var patient = new Patient() { Id = 1, ChCorrection = 10 };
        var chConsumed = 30;

        A.CallTo(() => _userRepository.GetPatientInfo(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _eventRepository.GetEventByIdAsync(foodEvent.IdCargaEventoNavigation.Id)).Returns(Task.FromResult(new CargaEvento()));
        A.CallTo(() => _eventRepository.EditFoodEventAsync(foodEvent)).Returns(Task.FromResult((float)chConsumed));

        var result = await _foodService.EditFoodManuallyEventAsync(email, foodEvent);

        Assert.Equal(chConsumed, result.ChConsumed);
        Assert.Equal((float)chConsumed / patient.ChCorrection, result.InsulinRecomended);
    }

    [Fact]
    public async Task EditFoodManuallyEventAsync_WhenCalledInvalidPatient_ThrowsPatientNotFoundException()
    {
        var email = "invalid@example.com";
        var foodEvent = new EventoComidum
        {
            IdCargaEventoNavigation = new CargaEvento { Id = 1 }
        };

        A.CallTo(() => _userRepository.GetPatientInfo(email)).Throws(new PatientNotFoundException());

        await Assert.ThrowsAsync<PatientNotFoundException>(() => _foodService.EditFoodManuallyEventAsync(email, foodEvent));
    }

    [Fact]
    public async Task EditFoodManuallyEventAsync_WhenCalledValidPatientInvalidEvent_ThrowsEventNotRelatedWithPatientException()
    {
        var email = "valid@example.com";
        var foodEvent = new EventoComidum
        {
            IdCargaEventoNavigation = new CargaEvento { Id = 1 }
        };
        var patient = new Patient() { Id = 1, ChCorrection = 10 };
        var loadedEvent = new CargaEvento { Id = 1 };

        A.CallTo(() => _userRepository.GetPatientInfo(email)).Returns(Task.FromResult(patient));
        A.CallTo(() => _eventRepository.GetEventByIdAsync(foodEvent.IdCargaEventoNavigation.Id)).Returns(Task.FromResult(loadedEvent));
        A.CallTo(() => _patientEventValidator.ValidatePatientEvent(email, loadedEvent)).Throws(new EventNotRelatedWithPatientException());

        await Assert.ThrowsAsync<EventNotRelatedWithPatientException>(() => _foodService.EditFoodManuallyEventAsync(email, foodEvent));
    }

    [Fact]
    public async Task GetIngredients_ShouldReturnListOfIngredients()
    {
        // Arrange
        var ingredients = new List<AdditionalDataIngredient>
        {
            new AdditionalDataIngredient { Name = "Ingredient1" },
            new AdditionalDataIngredient { Name = "Ingredient2" }
        };

        A.CallTo(() => _eventRepository.GetIngredients()).Returns(Task.FromResult<IEnumerable<AdditionalDataIngredient>>(ingredients));

        // Act
        var result = await _foodService.GetIngredients();

        // Assert
        Assert.Equal(ingredients, result);
    }

    [Fact]
    public async Task GetIngredients_ShouldCallGetIngredients()
    {
        // Act
        await _foodService.GetIngredients();

        // Assert
        A.CallTo(() => _eventRepository.GetIngredients()).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task AddFoodByTagEvent_ShouldCallAddFoodByTagEvent()
    {
        // Arrange
        var email = "test@example.com";
        var eventDate = DateTime.Now;
        var carbohydrates = 50;

        // Act
        await _foodService.AddFoodByTagEvent(email, eventDate, carbohydrates);

        // Assert
        A.CallTo(() => _eventRepository.AddFoodByTagEvent(email, eventDate, carbohydrates)).MustHaveHappenedOnceExactly();
    }
}
