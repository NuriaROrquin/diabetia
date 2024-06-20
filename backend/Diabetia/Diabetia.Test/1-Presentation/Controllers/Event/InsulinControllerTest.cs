using Diabetia.API.Controllers.Event;
using Diabetia.API.DTO.EventRequest.Insuline;
using Diabetia.Application.UseCases.EventUseCases;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Test.Presentation.Controllers.Event
{
    public class InsulinControllerTest
    {
        [Fact]
        public async Task AddInsulinAsync_ReturnsOk()
        {
            // Arrange
            var httpContextAccessor = A.Fake<IHttpContextAccessor>();
            var insulinUseCase = A.Fake<InsulinUseCase>();

            var controller = new InsulinController(httpContextAccessor, insulinUseCase);

            var addInsulinRequest = new AddInsulinRequest
            {
                KindEventId = 1,
                FreeNote = "Testing insulin event",
                InsulinInjected = 10,
                IdInsulinPatient = 1,
                EventDate = DateTime.Now
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, "test@example.com")
            }, "mock"));

            A.CallTo(() => httpContextAccessor.HttpContext.User)
                .Returns(user);

            var result = await controller.AddInsulinAsync(addInsulinRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Registro de insulina agregado correctamente", okResult.Value);
        }
    }
}
