using System.Collections.Generic;
using System.Threading.Tasks;
using Diabetia.Application.UseCases;
using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Services;
using FakeItEasy;
using Xunit;

namespace Diabetia_Core.Tag
{
    public class TagConfirmationUseCaseTests
    {
        private readonly IUserRepository _userRepository;
        private readonly FoodManuallyUseCase _foodManuallyUseCase;
        private readonly TagConfirmationUseCase _tagConfirmationUseCase;

        public TagConfirmationUseCaseTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _foodManuallyUseCase = A.Fake<FoodManuallyUseCase>();
            _tagConfirmationUseCase = new TagConfirmationUseCase(_userRepository, _foodManuallyUseCase);
        }

        [Fact]
        public async Task CalculateFoodResponseAsync_ReturnsCorrectResponses()
        {
            // Arrange
            var email = "test@example.com";
            var nutritionTags = new List<NutritionTag>
            {
                new NutritionTag { ChInPortion = 10, Portion = 2 },
                new NutritionTag { ChInPortion = 5, Portion = 3 }
            };

            var patientInfo = new Patient { ChCorrection = 15 };

            A.CallTo(() => _userRepository.GetPatientInfo(email)).Returns(Task.FromResult(patientInfo));

            // Act
            var result = await _tagConfirmationUseCase.CalculateFoodResponseAsync(email, nutritionTags);

            // Assert
            Assert.Equal(35, result.ChConsumed); // 10*2 + 5*3 = 35
            Assert.Equal(2.33f, result.InsulinRecomended); // 35 / 15 = 2.33
        }

        [Fact]
        public async Task CalculateFoodResponseAsync_EmptyTags_ReturnsZeroResponses()
        {
            // Arrange
            var email = "test@example.com";
            var nutritionTags = new List<NutritionTag>();

            var patientInfo = new Patient { ChCorrection = 15 };

            A.CallTo(() => _userRepository.GetPatientInfo(email)).Returns(Task.FromResult(patientInfo));

            // Act
            var result = await _tagConfirmationUseCase.CalculateFoodResponseAsync(email, nutritionTags);

            // Assert
            Assert.Equal(0, result.ChConsumed);
            Assert.Equal(0f, result.InsulinRecomended);
        }

        [Fact]
        public async Task CalculateFoodResponseAsync_InvalidChCorrection_ThrowsException()
        {
            // Arrange
            var email = "test@example.com";
            var nutritionTags = new List<NutritionTag>
            {
                new NutritionTag { ChInPortion = 10, Portion = 2 }
            };

            var patientInfo = new Patient { ChCorrection = 0 };

            A.CallTo(() => _userRepository.GetPatientInfo(email)).Returns(Task.FromResult(patientInfo));

            // Act & Assert
            await Assert.ThrowsAsync<DivideByZeroException>(() => _tagConfirmationUseCase.CalculateFoodResponseAsync(email, nutritionTags));
        }
    }
}
