using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using FakeItEasy;
using Refit;

namespace Diabetia_Core.FoodDish
{
    public class FoodDishDetectionUseCaseTests
    {
        private readonly IFoodDishProvider _foodDishProvider;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly FoodDishDetectionUseCase _useCase;

        public FoodDishDetectionUseCaseTests()
        {
            _foodDishProvider = A.Fake<IFoodDishProvider>();
            _userRepository = A.Fake<IUserRepository>();
            _eventRepository = A.Fake<IEventRepository>();
            _useCase = new FoodDishDetectionUseCase(_foodDishProvider, _userRepository, _eventRepository);
        }

        [Fact]
        public async Task DetectFoodDish_ShouldReturnDetectedFoodDish()
        {
            // Arrange
            var foodDish = new Diabetia.Domain.Entities.FoodDish() { /* Properties */ };
            var foodImageBase64 = new Diabetia.Domain.Entities.FoodDish() { ImageBase64 = "base64String" };

            A.CallTo(() => _foodDishProvider.DetectFoodDish(A<StreamPart>.That.Matches(sp => sp.FileName == "image.jpg"))).Returns(Task.FromResult(foodDish));

            // Act
            var result = await _useCase.DetectFoodDish(foodImageBase64);

            // Assert
            A.CallTo(() => _foodDishProvider.DetectFoodDish(A<StreamPart>.That.Matches(sp => sp.FileName == "image.jpg"))).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ConfirmDish_ShouldReturnIngredientsDetected()
        {
            // Arrange
            var foodDish = new Diabetia.Domain.Entities.FoodDish() { /* Properties */ };
            var ingredientsDetected = new IngredientsDetected { /* Properties */ };

            A.CallTo(() => _foodDishProvider.GetNutrientPerIngredient(foodDish)).Returns(Task.FromResult(ingredientsDetected));

            // Act
            var result = await _useCase.ConfirmDish(foodDish);

            // Assert
            A.CallTo(() => _foodDishProvider.GetNutrientPerIngredient(foodDish)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task SaveFoodEvent_ShouldReturnFoodResultsEvent()
        {
            // Arrange
            var email = "test@example.com";
            var ingredientsConfirmed = new List<FoodInfo>
            {
                new FoodInfo { Carbohydrates = 10, Quantity = 2 },
                new FoodInfo { Carbohydrates = 5, Quantity = 1 }
            };
            var userPatientInfo = new Patient() { Id = 1, ChCorrection = 15 };

            A.CallTo(() => _userRepository.GetPatientInfo(email)).Returns(Task.FromResult(userPatientInfo));
            A.CallTo(() => _eventRepository.AddFoodByDetectionEvent(userPatientInfo.Id, A<DateTime>.Ignored, A<float>.Ignored)).Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.SaveFoodEvent(email, ingredientsConfirmed);

            // Assert
            A.CallTo(() => _userRepository.GetPatientInfo(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _eventRepository.AddFoodByDetectionEvent(userPatientInfo.Id, A<DateTime>.Ignored, 25f)).MustHaveHappenedOnceExactly();
            Assert.NotNull(result);
        }
    }
}
