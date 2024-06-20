using Diabetia.API.Controllers;
using Diabetia.API.DTO;
using Diabetia.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using FakeItEasy;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Test._1_Presentation.Controllers
{
    /*
    public class EventControllerTest
    {
        
        [Fact]
        public async Task AddInsulinEvent_ShouldReturnOk()
        {
            var FakePhysicaUseCase = A.Fake<AddPhysicalEventUseCase>();
            var FakeGlucoseUseCase = A.Fake<AddGlucoseEventUseCase>();
            var FakeInsulineUseCase = A.Fake<AddInsulinEventUseCase>();
            var controller = new EventController(FakePhysicaUseCase, FakeGlucoseUseCase, FakeInsulineUseCase);

            var request = new InsulinEventRequest
            {
                Email = "test@example.com",
                IdKindEvent = 1,
                EventDate = DateTime.Now,
                FreeNote = "Nota libre",
                Insulin = 7
            };

            // Act
            var result = await controller.AddInsulinEvent(request);

            // Assert
            A.CallTo(() => FakeInsulineUseCase.AddInsulinEvent(request.Email, request.IdKindEvent, request.EventDate, request.FreeNote, request.Insulin))
                .MustHaveHappenedOnceExactly();

            Assert.IsType<OkResult>(result);
        }
    }*/
}
