using Moq;
using Diabetia.Infrastructure.Repositories;
using Diabetia.Domain.Models;
using Moq.EntityFrameworkCore;
using Diabetia.Infrastructure.EF;
using Diabetia.Domain.Exceptions;

namespace Diabetia_Infrastructure.Repositories.Events
{
    public class EventRepositoryTests
    {
        // --------------------------------------- ⬇⬇ Add PhysicalActivityEvent Test ⬇⬇ ---------------------------------------
        [Fact]
        public async Task AddPhysicalActivityEventAsync_GivenValidData_ShouldAddEventAndPhysicalEvent()
        {
            // Arrange
            var mockContext = CreateMockContextAddPassCorrect();

            var repository = new EventRepository(mockContext.Object);

            var physicalActivity = new EventoActividadFisica()
            {
                IdActividadFisica = 1,
                Duracion = 50,
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
            int patientId = 1;

            // Act
            await repository.AddPhysicalActivityEventAsync(patientId, physicalActivity);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            mockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoActividadFisicas.Add(It.IsAny<EventoActividadFisica>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextAddPassCorrect()
        {
            var patient = new Paciente { Id = 1 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica>());

            return mockContext;
        }


        // --------------------------------------- ⬇⬇ Edit PhysicalActivityEvent Test ⬇⬇ ---------------------------------------
        [Fact]
        public async Task EditPhysicalActivityEvent_ShouldUpdateEventAndPhysicalEvent()
        {
            // Arrange
            var mockContext = CreateMockContextEditPassCorrect();

            var fakerepository = new EventRepository(mockContext.Object);

            var physicalActivity = new EventoActividadFisica()
            {
                IdActividadFisica = 1,
                Duracion = 50,
                IdCargaEventoNavigation = new CargaEvento ()
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

            // Act
            await fakerepository.EditPhysicalActivityEventAsync(physicalActivity);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            var @event = mockContext.Object.CargaEventos.First();
            var physicalEvent = mockContext.Object.EventoActividadFisicas.First();

            Assert.Equal(physicalActivity.IdCargaEventoNavigation.FechaEvento, @event.FechaEvento);
            Assert.Equal(physicalActivity.IdCargaEventoNavigation.NotaLibre, @event.NotaLibre);
            Assert.Equal(50, physicalEvent.Duracion);
        }
        private Mock<diabetiaContext> CreateMockContextEditPassCorrect()
        {
            var @event = new CargaEvento { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "Test Note" };
            var physicalEvent = new EventoActividadFisica { IdCargaEvento = @event.Id, IdActividadFisica = 1, Duracion = 50, IdCargaEventoNavigation = @event };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalEvent });

            return mockContext;
        }

        [Fact]
        public async Task EditPhysicalActivityEvent_GivenInvalidEvent_ThrowsPhysicalEventNotMatchException()
        {
            var mockContext = CreateMockContextThrowPhysicalEventException();

            var fakeRepository = new EventRepository(mockContext.Object);

            var physicalActivity = new EventoActividadFisica()
            {
                IdActividadFisica = 1,
                Duracion = 50,
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

            // Act & Assert

            await Assert.ThrowsAsync<PhysicalEventNotMatchException>(async () =>
            await fakeRepository.EditPhysicalActivityEventAsync(physicalActivity));
        }
        private Mock<diabetiaContext> CreateMockContextThrowPhysicalEventException()
        {
            var @event = new CargaEvento { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "nota Test" };
            var physicalActivityEvent = new EventoActividadFisica { Id = 1, IdCargaEvento = 2, IdActividadFisica = 4, Duracion = 60 };
            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalActivityEvent });

            return mockContext;
        }

        // --------------------------------------- ⬇⬇ Delete PhysicalActivityEvent Test ⬇⬇ ---------------------------------------
        [Fact]
        public async Task DeletePhysicalActivityEvent_GivenValidData_ShouldDeleteEventSuccessfully()
        {
            var mockContext = CreateMockContextDeletePassCorrect();
            var fakeRepository = new EventRepository(mockContext.Object);
            var cargaEvento = new CargaEvento
            {
                Id = 1,
            };

            await fakeRepository.DeletePhysicalActivityEventAsync(cargaEvento.Id);

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.CargaEventos.Remove(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoActividadFisicas.Remove(It.IsAny<EventoActividadFisica>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextDeletePassCorrect()
        {
            var @event = new CargaEvento { Id = 1, IdPaciente = 11, FechaEvento = DateTime.Now, NotaLibre = "Testing Note" };
            var physicalActivityEvent = new EventoActividadFisica { Id = 1, IdCargaEvento = @event.Id, Duracion = 50 };
            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalActivityEvent });

            return mockContext;
        }
    }
}