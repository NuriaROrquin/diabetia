using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;


namespace Diabetia_Infrastructure.Repositories.Events
{
    public class InsulineTest
    {
        [Fact]
        public async Task AddInsulinEventAsync_GivenValidData_ShouldAddAInsulinEvent()
        {
            var mockContext = CreateMockContextAddPassCorrect();

            var repository = new EventRepository(mockContext.Object);

            var insulin = new EventoInsulina()
            {
                InsulinaInyectada = 7,
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 1,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test Note",
                    FechaActual = DateTime.Now,
                    FueRealizado = false,
                    EsNotaLibre = false
                }
            };
            int patientId = 11;

            await repository.AddInsulinEventAsync(patientId, insulin);

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            mockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoInsulinas.Add(It.IsAny<EventoInsulina>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextAddPassCorrect()
        {
            var patient = new Paciente { Id = 1 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
            mockContext.Setup(m => m.EventoInsulinas).ReturnsDbSet(new List<EventoInsulina>());

            return mockContext;
        }
        

        [Fact]
        public async Task EditInsulinEvent_ShouldUpdateEventAndInsulinEvent()
        {
            // Arrange
            var mockContext = CreateMockContextEditPassCorrect();

            var fakerepository = new EventRepository(mockContext.Object);

            var insulin = new EventoInsulina()
            {
                InsulinaInyectada = 8,
                IdCargaEventoNavigation = new CargaEvento
                {
                    Id = 1,
                    IdTipoEvento = 1,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test Note",
                    FechaActual = DateTime.Now,
                    FueRealizado = false,
                    EsNotaLibre = false
                }
            };

            await fakerepository.EditInsulinEventAsync(insulin);

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            var loadedEvent = mockContext.Object.CargaEventos.First();
            var insulinEvent = mockContext.Object.EventoInsulinas.First();

            Assert.Equal(insulin.IdCargaEventoNavigation.FechaEvento, loadedEvent.FechaEvento);
            Assert.Equal(insulin.IdCargaEventoNavigation.NotaLibre, loadedEvent.NotaLibre);
            Assert.Equal(8, insulinEvent.InsulinaInyectada);
        }
        private Mock<diabetiaContext> CreateMockContextEditPassCorrect()
        {
            var loadedEvent = new CargaEvento { Id = 1, IdPaciente = 11, FechaEvento = DateTime.Now, NotaLibre = "Edit Test Note" };
            var insulinEvent = new EventoInsulina { IdCargaEvento = loadedEvent.Id, InsulinaInyectada = 4 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { loadedEvent });
            mockContext.Setup(m => m.EventoInsulinas).ReturnsDbSet(new List<EventoInsulina> { insulinEvent });

            return mockContext;
        }
        
        [Fact]
        public async Task EditInsulinEvent_GivenInvalidEvent_ThrowsInsulinEventNotMatchException()
        {
            var mockContext = CreateMockContextThrowGlucoseEventException();

            var fakeRepository = new EventRepository(mockContext.Object);

            var insulin = new EventoInsulina()
            {
                InsulinaInyectada = 8,
                IdCargaEventoNavigation = new CargaEvento
                {
                    Id = 1,
                    IdTipoEvento = 1,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test Note",
                    FechaActual = DateTime.Now,
                    FueRealizado = false,
                    EsNotaLibre = false
                }
            };

            await Assert.ThrowsAsync<GlucoseEventNotMatchException>(async () =>
            await fakeRepository.EditInsulinEventAsync(insulin));
        }
        private Mock<diabetiaContext> CreateMockContextThrowGlucoseEventException()
        {
            var loadedEvent = new CargaEvento { Id = 1, IdPaciente = 11, FechaEvento = DateTime.Now, NotaLibre = "Edit Test Note" };
            var insulinEvent = new EventoInsulina { Id = 1, IdCargaEvento = 3, InsulinaInyectada = 20 };
            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { loadedEvent });
            mockContext.Setup(m => m.EventoInsulinas).ReturnsDbSet(new List<EventoInsulina> { insulinEvent });

            return mockContext;
        }
        
        [Fact]
        public async Task DeleteInsulinEventAsync_ShouldDeleteInsulinEventAndRelatedCargaEvent()
        {
            var mockContext = CreateMockContextDeletePassCorrect();
            var fakeRepository = new EventRepository(mockContext.Object);

            var cargaEvento = new CargaEvento
            {
                Id = 1,
            };

            await fakeRepository.DeleteInsulinEventAsync(cargaEvento.Id);

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.CargaEventos.Remove(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoInsulinas.Remove(It.IsAny<EventoInsulina>()), Times.Once);
        }

        private Mock<diabetiaContext> CreateMockContextDeletePassCorrect()
        {
            var loadedEvent = new CargaEvento { Id = 1, IdPaciente = 11, FechaEvento = DateTime.Now, NotaLibre = "Edit Test Note" };
            var insulinEvent = new EventoInsulina { Id = 1, IdCargaEvento = 1, InsulinaInyectada = 200 };
            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { loadedEvent });
            mockContext.Setup(m => m.EventoInsulinas).ReturnsDbSet(new List<EventoInsulina> { insulinEvent });

            return mockContext;
        }
    }
}
