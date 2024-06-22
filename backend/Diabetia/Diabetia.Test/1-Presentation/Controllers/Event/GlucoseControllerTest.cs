using Diabetia.API.Controllers.Event;
using Diabetia.API.DTO.EventRequest.Glucose;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FakeItEasy;

namespace Diabetia_Presentation.Events
{
    public class GlucoseControllerTests
    {
        [Fact]
        public async Task AddGlucoseAsync_ShouldReturnOkResult()
        {
            var httpContextAccessor = A.Fake<IHttpContextAccessor>();
            var glucoseUseCase = A.Fake<GlucoseUseCase>();

            var context = new DefaultHttpContext();
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Email, "test@example.com")
            });
            context.User = new ClaimsPrincipal(claimsIdentity);
            A.CallTo(() => httpContextAccessor.HttpContext).Returns(context);

            var glucoseController = new GlucoseController(httpContextAccessor, glucoseUseCase);

            var addGlucoseRequest = new AddGlucoseRequest
            {
                KindEventId = 3,
                FreeNote = "string",
                Glucose = 165,
                PatientDeviceId = null,
                IdFoodEvent = null,
                PostFoodMedition = false
            };

            var result = await glucoseController.AddGlucoseAsync(addGlucoseRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Registro de glucosa agregado correctamente", okResult.Value);
        }

        [Fact]
        public async Task EditMedicalEventAsync_ShouldReturnOkResult()
        {
            var httpContextAccessor = A.Fake<IHttpContextAccessor>();
            var glucoseUseCase = A.Fake<GlucoseUseCase>();

            var context = new DefaultHttpContext();
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Email, "test@example.com")
            });
            context.User = new ClaimsPrincipal(claimsIdentity);
            A.CallTo(() => httpContextAccessor.HttpContext).Returns(context);

            var controller = new GlucoseController(httpContextAccessor, glucoseUseCase);

            var request = new EditGlucoseRequest
            {
                FreeNote = "string",
                Glucose = 165,
                PatientDeviceId = null,
                IdFoodEvent = null,
                PostFoodMedition = false
            };

            var result = await controller.EditGlucoseEventAsync(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Registro de glucosa modificado correctamente", okResult.Value);
        }
    }


}
