using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

namespace Diabetia_Infrastructure.Repositories.Reporting
{
    public class PhysicalActivityTest
    {
        [Fact]
        public async Task GetPhysicalActivityEventSummaryByPatientId_GivingValidData_ShouldGetActivitySuccessfully()
        {
            // Arrange
            var mockContext = CreateMockContextForPhysicalReporting();
            var fakeRepository = new ReportingRepository(mockContext.Object);
            var patientId = 1;
            var dateFrom = DateTime.Now.AddDays(1);
            var dateTo = DateTime.Now.AddDays(3);

            //Act
            var result = await fakeRepository.GetPhysicalActivityEventSummaryByPatientId(patientId, dateFrom, dateTo);

            //Assert
            Assert.NotNull(result);
            Assert.Single(result);

        }

        private Mock<diabetiaContext> CreateMockContextForPhysicalReporting()
        {
            var @event = new CargaEvento() { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now.AddDays(2) };
            var physicalActivity = new ActividadFisica() { Id = 1, Nombre = "Test" };
            var eventPhysicalActivity = new EventoActividadFisica() { Id = 1, IdActividadFisica = physicalActivity.Id, IdCargaEvento = @event.Id };

            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.ActividadFisicas).ReturnsDbSet(new List<ActividadFisica> { physicalActivity });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { eventPhysicalActivity });

            return mockContext;
        }     
    }
}
