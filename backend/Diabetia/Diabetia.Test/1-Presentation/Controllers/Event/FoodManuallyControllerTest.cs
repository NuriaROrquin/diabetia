using Diabetia.API.Controllers.Event;
using Diabetia.API.DTO.EventRequest.Food;
using Diabetia.API.DTO.EventRequest.Glucose;
using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System.Security.Claims;


namespace Diabetia.Test._1_Presentation.Controllers.Event
{
    /*
    public class FoodManuallyControllerTest
    {
        [Fact]
        public async Task AddFoodManuallyAsync_ShouldReturnOkResult()
        {
            // Arrange
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var foodManuallyUseCase = new Mock<FoodManuallyUseCase>();

            var context = new DefaultHttpContext();
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Email, "test@example.com")
            });
            context.User = new ClaimsPrincipal(claimsIdentity);
            httpContextAccessor.Setup(x => x.HttpContext).Returns(context);

            var foodManuallyController = new FoodManuallyController(httpContextAccessor.Object, foodManuallyUseCase.Object);

            var request = new AddFoodManuallyRequest
            {
                KindEventId = 2,
                FreeNote = "Test Note",
                Ingredients = new List<Ingredient>
            {
                new Ingredient { IdIngredient = 186, Quantity = 100 }
            }
            };

            var foodEventResponse = new FoodResultsEvent
            {
                ChConsumed = 50,
                InsulinRecomended = 2.5f
            };

            foodManuallyUseCase.Setup(x => x.AddFoodManuallyEventAsync(It.IsAny<string>(), It.IsAny<EventoComidum>()))
                              .ReturnsAsync(foodEventResponse);

            // Act
            var result = await foodManuallyController.AddFoodManuallyAsync(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<AddFoodResponse>(okResult.Value);
            Assert.Equal(foodEventResponse.ChConsumed, response.ChConsumed);
            Assert.Equal(foodEventResponse.InsulinRecomended, response.InsulinRecomended);
        }
    }
    */
}
