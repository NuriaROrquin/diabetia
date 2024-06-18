using Moq;
using Diabetia.Infrastructure.Repositories;
using Diabetia.Domain.Models;
using Moq.EntityFrameworkCore;
using Diabetia.Infrastructure.EF;
using Diabetia.Domain.Exceptions;

namespace Diabetia_Infrastructure
{
    public class EventRepositoryTests
    {
        // --------------------------------------- AddPhysicalActivityEvent Test ---------------------------------------
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


        // --------------------------------------- EditPhysicalActivityEvent Test ---------------------------------------
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
            var physicalEvent = new EventoActividadFisica { IdCargaEvento = @event.Id, IdActividadFisica = 1, Duracion = 50 };

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


        //        // --------------------------------------- DeletePhysicalActivityEvent Test ---------------------------------------
        //        [Fact]
        //        public async Task DeletePhysicalActivityEventAsync_GivenValidInformation_ShouldDeleteSuccessfully()
        //        {
        //            // Arrange
        //            var mockContextBeforeDeletion = CreateMockContextBeforeDeletion();
        //            var mockContextAfterDeletion = CreateMockContextAfterDeletion();
        //            var repositoryEvent = new EventRepository(mockContextBeforeDeletion.Object);
        //            var eventId = 1;

        //            // Act
        //            await repositoryEvent.DeletePhysicalActivityEventAsync(eventId);

        //            // Assert
        //            mockContextBeforeDeletion.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //            mockContextBeforeDeletion.Verify(m => m.EventoActividadFisicas.Remove(It.IsAny<EventoActividadFisica>()), Times.Once);
        //            mockContextBeforeDeletion.Verify(m => m.CargaEventos.Remove(It.IsAny<CargaEvento>()), Times.Once);


        //            var deletedEvent = await mockContextAfterDeletion.Object.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == eventId);
        //            Assert.Null(deletedEvent);

        //            var deletedPhysicalEvent = await mockContextAfterDeletion.Object.EventoActividadFisicas.FirstOrDefaultAsync(eaf => eaf.IdCargaEvento == eventId);
        //            Assert.Null(deletedPhysicalEvent);
        //        }
        //        private Mock<diabetiaContext> CreateMockContextBeforeDeletion()
        //        {
        //            var @event = new CargaEvento { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "Old Note" };
        //            var physicalEvent = new EventoActividadFisica { IdCargaEvento = @event.Id, IdActividadFisica = 1, Duracion = 60 };

        //            var mockContext = new Mock<diabetiaContext>();

        //            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
        //            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalEvent });

        //            return mockContext;
        //        }
        //        private Mock<diabetiaContext> CreateMockContextAfterDeletion()
        //        {
        //            var mockContextAfterDeletion = new Mock<diabetiaContext>();
        //            mockContextAfterDeletion.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
        //            mockContextAfterDeletion.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica>());

        //            return mockContextAfterDeletion;
        //        }

        //        [Fact]
        //        public async Task DeletePhysicalActivityEventAsync_GivenInvalidEventId_ThrowsEventNotFoundException()
        //        {
        //            // Arrange
        //            var mockContext = MockContextDeleteThrowEventException();

        //            var eventRepository = new EventRepository(mockContext.Object);

        //            var eventId = 1;

        //            // Act & Assert
        //            await Assert.ThrowsAsync<EventNotFoundException>(async () =>
        //            await eventRepository.DeletePhysicalActivityEventAsync(eventId));
        //        }
        //        private Mock<diabetiaContext> MockContextDeleteThrowEventException()
        //        {
        //            var @event = new CargaEvento { Id = 2, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "nota Test" };
        //            var mockContext = new Mock<diabetiaContext>();

        //            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });

        //            return mockContext;
        //        }

        //        [Fact]
        //        public async Task DeletePhysicalActivityEventAsync_GivenValidEventNotMatchPhysicalActivity_ThrowsPhysicalEventNotMatchException()
        //        {
        //            // Arrange
        //            var mockContext = MockContextDeleteThrowPhysicalMismatchException();

        //            var eventRepository = new EventRepository(mockContext.Object);

        //            var eventId = 1;

        //            // Act & Assert
        //            await Assert.ThrowsAsync<PhysicalEventNotMatchException>(async () =>
        //            await eventRepository.DeletePhysicalActivityEventAsync(eventId));
        //        }
        //        private Mock<diabetiaContext> MockContextDeleteThrowPhysicalMismatchException()
        //        {
        //            var @event = new CargaEvento { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "nota Test" };
        //            var physicalActivityEvent = new EventoActividadFisica { Id = 1, IdCargaEvento = 2, IdActividadFisica = 4, Duracion = 60 };

        //            var mockContext = new Mock<diabetiaContext>();

        //            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
        //            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalActivityEvent });

        //            return mockContext;
        //        }
        //    }
        //}
    }
}