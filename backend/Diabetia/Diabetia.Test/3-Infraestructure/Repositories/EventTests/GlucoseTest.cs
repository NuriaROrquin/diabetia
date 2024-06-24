using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Diabetia_Infrastructure.Repositories.Events
{
    public class GlucoseTest
    {
        [Fact]
        public async Task AddGlucoseEventAsync_GivenValidData_ShouldAddAGlucoseEvent()
        {
            var mockContext = CreateMockContextAddPassCorrect();

            var repository = new EventRepository(mockContext.Object);

            var glucose = new EventoGlucosa()
            {
                Glucemia = 160,
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 3,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test Note",
                    FechaActual = DateTime.Now,
                    FueRealizado = false,
                    EsNotaLibre = false
                }
            };
            int patientId = 11;

            await repository.AddGlucoseEventAsync(patientId, glucose);

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            mockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoGlucosas.Add(It.IsAny<EventoGlucosa>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextAddPassCorrect()
        {
            var patient = new Paciente { Id = 1 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
            mockContext.Setup(m => m.EventoGlucosas).ReturnsDbSet(new List<EventoGlucosa>());

            return mockContext;
        }

        [Fact]
        public async Task EditGlucoseEvent_ShouldUpdateEventAndGlucoseEvent()
        {
            // Arrange
            var mockContext = CreateMockContextEditPassCorrect();

            var fakerepository = new EventRepository(mockContext.Object);

            var glucose = new EventoGlucosa()
            {
                Glucemia = 180,
                IdCargaEventoNavigation = new CargaEvento
                {
                    Id = 1,
                    IdTipoEvento = 3,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test Note",
                    FechaActual = DateTime.Now,
                    FueRealizado = false,
                    EsNotaLibre = false
                }
            };

            await fakerepository.EditGlucoseEventAsync(glucose);

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            var loadedEvent = mockContext.Object.CargaEventos.First();
            var glucoseEvent = mockContext.Object.EventoGlucosas.First();

            Assert.Equal(glucose.IdCargaEventoNavigation.FechaEvento, loadedEvent.FechaEvento);
            Assert.Equal(glucose.IdCargaEventoNavigation.NotaLibre, loadedEvent.NotaLibre);
            Assert.Equal(180, glucoseEvent.Glucemia);
        }
        private Mock<diabetiaContext> CreateMockContextEditPassCorrect()
        {
            var loadedEvent = new CargaEvento { Id = 1, IdPaciente = 11, FechaEvento = DateTime.Now, NotaLibre = "Edit Test Note" };
            var glucoseEvent = new EventoGlucosa { IdCargaEvento = loadedEvent.Id, Glucemia = 200};

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { loadedEvent });
            mockContext.Setup(m => m.EventoGlucosas).ReturnsDbSet(new List<EventoGlucosa> { glucoseEvent });

            return mockContext;
        }

        [Fact]
        public async Task EditGlucoseEvent_GivenInvalidEvent_ThrowsGlucoseEventNotMatchException()
        {
            var mockContext = CreateMockContextThrowGlucoseEventException();

            var fakeRepository = new EventRepository(mockContext.Object);

            var glucose = new EventoGlucosa()
            {
                Glucemia = 180,
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
            await fakeRepository.EditGlucoseEventAsync(glucose));
        }
        private Mock<diabetiaContext> CreateMockContextThrowGlucoseEventException()
        {
            var loadedEvent = new CargaEvento { Id = 1, IdPaciente = 11, FechaEvento = DateTime.Now, NotaLibre = "Edit Test Note" };
            var glucoseEvent = new EventoGlucosa { Id = 1, IdCargaEvento = 3, Glucemia = 200};
            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { loadedEvent });
            mockContext.Setup(m => m.EventoGlucosas).ReturnsDbSet(new List<EventoGlucosa> { glucoseEvent });

            return mockContext;
        }

        [Fact]
        public async Task DeleteGlucoseEventAsync_ShouldDeleteGlucoseEventAndRelatedCargaEvent()
        {
            var mockContext = CreateMockContextDeletePassCorrect();
            var fakeRepository = new EventRepository(mockContext.Object);

            var cargaEvento = new CargaEvento
            {
                Id = 1,
            };

            await fakeRepository.DeleteGlucoseEventAsync(cargaEvento.Id);

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.CargaEventos.Remove(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoGlucosas.Remove(It.IsAny<EventoGlucosa>()), Times.Once);
        }

        private Mock<diabetiaContext> CreateMockContextDeletePassCorrect()
        {
            var loadedEvent = new CargaEvento { Id = 1, IdPaciente = 11, FechaEvento = DateTime.Now, NotaLibre = "Edit Test Note" };
            var glucoseEvent = new EventoGlucosa { Id = 1, IdCargaEvento = 1, Glucemia = 200 };
            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { loadedEvent });
            mockContext.Setup(m => m.EventoGlucosas).ReturnsDbSet(new List<EventoGlucosa> { glucoseEvent });

            return mockContext;
        }

    }
}
