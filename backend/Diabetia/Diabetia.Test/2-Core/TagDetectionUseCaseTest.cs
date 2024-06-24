using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.Tag
{
    public class TagDetectionUseCaseTests
    {
        private readonly ITagRecognitionProvider _apiAmazonService;
        private readonly IPatientValidator _patientValidator;
        private readonly TagDetectionUseCase _tagDetectionUseCase;

        public TagDetectionUseCaseTests()
        {
            _apiAmazonService = A.Fake<ITagRecognitionProvider>();
            _patientValidator = A.Fake<IPatientValidator>();
            _tagDetectionUseCase = new TagDetectionUseCase(_apiAmazonService, _patientValidator);
        }

        [Fact]
        public async Task GetOcrResponseFromDocument_ReturnsNutritionTags()
        {
            // Arrange
            var email = "test@example.com";
            var tagRequests = new List<string> { "document1", "document2" };

            A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);

            var nutritionTag1 = new NutritionTag { CarbohydratesText = "Carbohidratos 25 g Porción 100 g", UniqueId = "1" };
            var nutritionTag2 = new NutritionTag { CarbohydratesText = "Carbohidratos 15 g Porción 50 g", UniqueId = "2" };

            A.CallTo(() => _apiAmazonService.GetChFromDocument("document1")).Returns(Task.FromResult(nutritionTag1));
            A.CallTo(() => _apiAmazonService.GetChFromDocument("document2")).Returns(Task.FromResult(nutritionTag2));

            // Act
            var result = await _tagDetectionUseCase.GetOcrResponseFromDocument(email, tagRequests);

            // Assert
            var resultList = new List<NutritionTag>(result);

            Assert.Equal(2, resultList.Count);
            Assert.Equal(25, resultList[0].ChInPortion);
            Assert.Equal(100, resultList[0].GrPerPortion);
            Assert.Equal(15, resultList[1].ChInPortion);
            Assert.Equal(50, resultList[1].GrPerPortion);
        }

        [Fact]
        public async Task GetOcrResponseFromDocument_ThrowsGrPerPortionNotFoundException()
        {
            // Arrange
            var email = "test@example.com";
            var tagRequests = new List<string> { "document1" };

            A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);

            var nutritionTag = new NutritionTag { CarbohydratesText = "Carbohidratos 25 g", UniqueId = "1" };

            A.CallTo(() => _apiAmazonService.GetChFromDocument("document1")).Returns(Task.FromResult(nutritionTag));

            // Act & Assert
            await Assert.ThrowsAsync<GrPerPortionNotFoundException>(() => _tagDetectionUseCase.GetOcrResponseFromDocument(email, tagRequests));
        }

        [Fact]
        public async Task GetOcrResponseFromDocument_ThrowsChPerPortionNotFoundException()
        {
            // Arrange
            var email = "test@example.com";
            var tagRequests = new List<string> { "document1" };

            A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);

            var nutritionTag = new NutritionTag { CarbohydratesText = "Porción 100 g", UniqueId = "1" };

            A.CallTo(() => _apiAmazonService.GetChFromDocument("document1")).Returns(Task.FromResult(nutritionTag));

            // Act & Assert
            await Assert.ThrowsAsync<ChPerPortionNotFoundException>(() => _tagDetectionUseCase.GetOcrResponseFromDocument(email, tagRequests));
        }

        [Fact]
        public async Task GetOcrResponseFromDocument_ValidatesPatient()
        {
            // Arrange
            var email = "test@example.com";
            var tagRequests = new List<string> { "document1" };

            A.CallTo(() => _patientValidator.ValidatePatient(email)).Returns(Task.CompletedTask);

            var nutritionTag = new NutritionTag { CarbohydratesText = "Carbohidratos 25 g Porción 100 g", UniqueId = "1" };

            A.CallTo(() => _apiAmazonService.GetChFromDocument("document1")).Returns(Task.FromResult(nutritionTag));

            // Act
            await _tagDetectionUseCase.GetOcrResponseFromDocument(email, tagRequests);

            // Assert
            A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }
    }
}
