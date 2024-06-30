using Diabetia.API.Controllers.Reporting;
using Diabetia.API.DTO.ReportingResponse.PhysicalActivity;
using Diabetia.Application.UseCases.ReportingUseCases;
using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Models;
using Diabetia.Domain.Utilities;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia_Presentation.Reporting
{
 /*   public class PhysicalActivityReportControllerTest
    {
        private readonly IHttpContextAccessor _fakeHttpContextAccessor;
        private readonly PhysicalActivityReportUseCase _fakePhysicalActivityAmountReportUseCase;
        private readonly PhysicalActivityReportController _realPhysicalActivityReportController;

        public PhysicalActivityReportControllerTest()
        {
            _fakeHttpContextAccessor = A.Fake<IHttpContextAccessor>();
            _fakePhysicalActivityAmountReportUseCase = A.Fake<PhysicalActivityReportUseCase>();

            // Usa la instancia real del controlador con los objetos simulados
            _realPhysicalActivityReportController = new PhysicalActivityReportController(_fakeHttpContextAccessor, _fakePhysicalActivityAmountReportUseCase);
        }

        [Fact]
        public async Task ShowPhysicalActivityDurationToReporting_GivingValidData_ShouldGetPhysicalActivitySuccessfully()
        {
            // Arrange
            var email = "test@diabetia.com";
            var dateFrom = DateTime.Now.AddDays(1);
            var dateTo = DateTime.Now.AddDays(3);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Email, "test@example.com")
            }, "mock"));

            var fakeEvent = new EventSummary
            {
                EventDay = DateTime.Now.Date,
                AmountEvents = 1
            };

            var fakeEvents = new List<EventSummary> { fakeEvent };

            var expectedResponse = new PhysicalActivityAmountResponse
            {
                Time = fakeEvent.EventDay.ToString("dd/MM/yyyy"),
                Value = fakeEvent.AmountEvents
            };

            A.CallTo(() => _fakeHttpContextAccessor.HttpContext.User).Returns(user);
            A.CallTo(() => _fakePhysicalActivityAmountReportUseCase.GetPhysicalActivityToReporting(email, dateFrom, dateTo)).Returns(Task.FromResult(fakeEvents));

            // Act
            var result = await _realPhysicalActivityReportController.ShowPhysicalActivitySummaryEventToReporting(new DateFilter
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            });

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var actualResponse = okResult.Value as List<PhysicalActivityAmountResponse>;
            Assert.NotNull(actualResponse);

            var actualItem = actualResponse.FirstOrDefault();
            Assert.Equal(expectedResponse.Time, actualItem.Time);
            Assert.Equal(expectedResponse.Value, actualItem.Value);
        }
    }*/

}
