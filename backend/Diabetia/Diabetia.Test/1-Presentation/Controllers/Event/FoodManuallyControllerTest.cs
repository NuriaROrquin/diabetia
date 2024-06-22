using Diabetia.API.Controllers.Event;
using Diabetia.API.DTO.EventRequest.Food;
using Diabetia.API.DTO.EventResponse.Food;
using Diabetia.Application.UseCases.EventUseCases;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;
using Diabetia.Domain.Utilities;
using Xunit;

namespace Diabetia_Presentation.Events
{
    public class FoodManuallyControllerTest
    {
        [Fact]
        public async Task AddFoodManuallyAsync_ReturnsOk()
        {
            // Arrange
            var date = new DateTime();
            
            var httpContextAccessor = A.Fake<IHttpContextAccessor>();
            var foodManuallyUseCase = A.Fake<FoodManuallyUseCase>();

            var controller = new FoodManuallyController(httpContextAccessor, foodManuallyUseCase);

            var addFoodManuallyRequest = new AddFoodManuallyRequest
            {
                KindEventId = (int)TypeEventEnum.COMIDA,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient{IdIngredient = 9, Quantity = 2}
                },
                FreeNote = "Test food",
                EventDate = date
            };

            var foodResponse = new FoodResultsEvent()
            {
                ChConsumed = 50,
                InsulinRecomended = 5
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, "test@example.com")
            }, "mock"));

            A.CallTo(() => httpContextAccessor.HttpContext.User).Returns(user);
            A.CallTo(() => foodManuallyUseCase.AddFoodManuallyEventAsync(A<string>.Ignored, A<EventoComidum>.Ignored))
                .Returns(Task.FromResult(foodResponse));
            
            // Act
            var result = await controller.AddFoodManuallyAsync(addFoodManuallyRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResponse = Assert.IsType<FoodResponse>(okResult.Value);
            
            Assert.Equal(foodResponse.ChConsumed, returnedResponse.ChConsumed);
            Assert.Equal(foodResponse.InsulinRecomended, returnedResponse.InsulinRecomended);
        }

        [Fact]
        public async Task EditFoodManuallyAsync_ShouldReturnOkResult()
        {
            // Arrange
            var date = new DateTime();

            var httpContextAccessor = A.Fake<IHttpContextAccessor>();
            var foodManuallyUseCase = A.Fake<FoodManuallyUseCase>();

            var controller = new FoodManuallyController(httpContextAccessor, foodManuallyUseCase);

            var editFoodManuallyRequest = new EditFoodManuallyRequest()
            {
                EventId = 5,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient{IdIngredient = 9, Quantity = 2}
                },
                FreeNote = "Test food",
                EventDate = date
            };

            var foodResponse = new FoodResultsEvent()
            {
                ChConsumed = 75,
                InsulinRecomended = (float)7.5
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, "test@example.com")
            }, "mock"));

            A.CallTo(() => httpContextAccessor.HttpContext.User).Returns(user);
            A.CallTo(() => foodManuallyUseCase.EditFoodManuallyEventAsync(A<string>.Ignored, A<EventoComidum>.Ignored))
                .Returns(Task.FromResult(foodResponse));

            // Act
            var result = await controller.EditMedicalEventAsync(editFoodManuallyRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResponse = Assert.IsType<FoodResponse>(okResult.Value);
            Assert.Equal(foodResponse.ChConsumed, returnedResponse.ChConsumed);
            Assert.Equal(foodResponse.InsulinRecomended, returnedResponse.InsulinRecomended);
        }
    }
}