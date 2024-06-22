using System.Threading.Tasks;
using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using Xunit;
using FakeItEasy;

namespace Diabetia_Core.Events;
public class FoodServiceTests
{
    private readonly IUserRepository _userRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IPatientValidator _patientValidator;
    private readonly FoodManuallyUseCase _foodService;

    public FoodServiceTests()
    {
        _userRepository = A.Fake<IUserRepository>();
        _eventRepository = A.Fake<IEventRepository>();
        _patientValidator = A.Fake<IPatientValidator>();
        _foodService = new FoodManuallyUseCase(_eventRepository, _patientValidator, _userRepository);
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
    /*
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
    }*/
}
