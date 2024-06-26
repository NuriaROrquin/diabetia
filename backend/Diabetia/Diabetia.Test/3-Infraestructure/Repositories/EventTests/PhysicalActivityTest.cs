﻿using Moq;
using Diabetia.Infrastructure.Repositories;
using Diabetia.Domain.Models;
using Moq.EntityFrameworkCore;
using Diabetia.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Diabetia.Infrastructure.EF;

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

            var email = "test@example.com";
            var kindEventId = 4;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivityId = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Updated Note";

            // Act
            await repository.AddPhysicalActivityEventAsync(email, kindEventId, eventDate, freeNote, physicalActivityId, iniciateTime, finishTime);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            mockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoActividadFisicas.Add(It.IsAny<EventoActividadFisica>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextAddPassCorrect()
        {
            var user = new Usuario { Id = 1, Email = "test@example.com" };
            var patient = new Paciente { Id = 1, IdUsuario = user.Id };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica>());

            return mockContext;
        }

        [Fact]
        public async Task AddPhysicalActivityEventAsync_GivenInvalidUser_ThrowsUserNotFoundOnDBException()
        {
            // Arrange
            var mockContext = CreateMockContextAddUserException();

            var repository = new EventRepository(mockContext.Object);

            var email = "testException@example.com";
            var kindEventId = 4;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivityId = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Updated Note";

            // Act & Assert
            await Assert.ThrowsAsync<UserNotFoundOnDBException>(async () =>
            await repository.AddPhysicalActivityEventAsync(email, kindEventId, eventDate, freeNote, physicalActivityId, iniciateTime, finishTime));
        }
        private Mock<diabetiaContext> CreateMockContextAddUserException()
        {
            var user = new Usuario { Id = 1, Email = "test@example.com" };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            return mockContext;
        }

        [Fact]
        public async Task AddPhysicalActivityEventAsync_GivenValidUserInvalidPatient_ThrowsPatientNotFoundException()
        {
            // Arrange
            var mockContext = CreateMockContextAddPatientException();

            var repository = new EventRepository(mockContext.Object);

            var email = "test@gmail.com";
            var kindEventId = 4;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivityId = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Updated Note";

            // Act & Assert
            await Assert.ThrowsAsync<PatientNotFoundException>(async () =>
            await repository.AddPhysicalActivityEventAsync(email, kindEventId, eventDate, freeNote, physicalActivityId, iniciateTime, finishTime));
        }
        private Mock<diabetiaContext> CreateMockContextAddPatientException()
        {
            var user = new Usuario { Id = 1, Email = "test@gmail.com" };
            var patient = new Paciente { Id = 1, IdUsuario = 2 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            return mockContext;
        }


        // --------------------------------------- EditPhysicalActivityEvent Test ---------------------------------------
        [Fact]
        public async Task EditPhysicalActivityEvent_ShouldUpdateEventAndPhysicalEvent()
        {
            // Arrange
            var mockContext = CreateMockContextEditPassCorrect();

            var repository = new EventRepository(mockContext.Object);

            var email = "test@example.com";
            var eventId = 1;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivity = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Updated Note";

            // Act
            await repository.EditPhysicalActivityEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            var @event = mockContext.Object.CargaEventos.First();
            var physicalEvent = mockContext.Object.EventoActividadFisicas.First();

            Assert.Equal(eventDate, @event.FechaEvento);
            Assert.Equal(freeNote, @event.NotaLibre);
            Assert.Equal(60, physicalEvent.Duracion);
        }
        private Mock<diabetiaContext> CreateMockContextEditPassCorrect()
        {
            var user = new Usuario { Id = 1, Email = "test@example.com" };
            var patient = new Paciente { Id = 1, IdUsuario = user.Id };
            var @event = new CargaEvento { Id = 1, IdPaciente = patient.Id, FechaEvento = DateTime.Now, NotaLibre = "Old Note" };
            var physicalEvent = new EventoActividadFisica { IdCargaEvento = @event.Id, IdActividadFisica = 1, Duracion = 60 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalEvent });

            return mockContext;
        }

        [Fact]
        public async Task EditPhysicalActivityEvent_GivenInvalidEvent_ThrowsEventNotFoundException()
        {
            var mockContext = CreateMockContextThrowEventException();

            var repository = new EventRepository(mockContext.Object);

            var email = "test@example.com";
            var eventId = 1;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivity = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Updated Note";

            // Act & Assert

            await Assert.ThrowsAsync<EventNotFoundException>(async () =>
            await repository.EditPhysicalActivityEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote));
        }
        private Mock<diabetiaContext> CreateMockContextThrowEventException()
        {
            var user = new Usuario { Id = 1, Email = "test@example.com" };
            var patient = new Paciente { Id = 1, IdUsuario = user.Id };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());

            return mockContext;
        }

        [Fact]
        public async Task EditPhysicalActivityEvent_GivenInvalidEvent_ThrowsUserEventNotFoundException()
        {
            var mockContext = CreateMockContextThrowUserException();
            var repository = new EventRepository(mockContext.Object);

            var email = "test@example.com";
            var eventId = 1;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivity = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Updated Note";

            // Act & Assert
            await Assert.ThrowsAsync<UserEventNotFoundException>(async () =>
            await repository.EditPhysicalActivityEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote));
        }
        private Mock<diabetiaContext> CreateMockContextThrowUserException()
        {
            var @event = new CargaEvento { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "nota Test" };

            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario>());

            return mockContext;
        }

        [Fact]
        public async Task EditPhysicalActivityEvent_GivenInvalidEvent_ThrowsPatientNotFoundException()
        {
            var mockContext = CreateMockContextThrowPatientException();

            var repository = new EventRepository(mockContext.Object);

            var email = "test@example.com";
            var eventId = 1;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivity = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Updated Note";

            // Act & Assert

            await Assert.ThrowsAsync<PatientNotFoundException>(async () =>
            await repository.EditPhysicalActivityEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote));
        }
        private Mock<diabetiaContext> CreateMockContextThrowPatientException()
        {
            var user = new Usuario { Id = 1, Email = "test@example.com" };
            var @event = new CargaEvento { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "nota Test" };

            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente>());

            return mockContext;
        }

        [Fact]
        public async Task EditPhysicalActivityEvent_GivenInvalidEvent_ThrowsEventNotRelatedWithPatientException()
        {
            var mockContext = CreateMockContextThrowEventPatientException();

            var repository = new EventRepository(mockContext.Object);

            var email = "test@example.com";
            var eventId = 1;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivity = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Updated Note";

            // Act & Assert

            await Assert.ThrowsAsync<EventNotRelatedWithPatientException>(async () =>
            await repository.EditPhysicalActivityEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote));
        }
        private Mock<diabetiaContext> CreateMockContextThrowEventPatientException()
        {
            var user = new Usuario { Id = 1, Email = "test@example.com" };
            var patient = new Paciente { Id = 1, IdUsuario = user.Id };
            var @event = new CargaEvento { Id = 1, IdPaciente = 2, FechaEvento = DateTime.Now, NotaLibre = "nota Test" };

            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });

            return mockContext;
        }

        [Fact]
        public async Task EditPhysicalActivityEvent_GivenInvalidEvent_ThrowsPhysicalEventNotMatchException()
        {
            var mockContext = CreateMockContextThrowPhysicalEventException();

            var repository = new EventRepository(mockContext.Object);

            var email = "test@example.com";
            var eventId = 1;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivity = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Updated Note";

            // Act & Assert

            await Assert.ThrowsAsync<PhysicalEventNotMatchException>(async () =>
            await repository.EditPhysicalActivityEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote));
        }
        private Mock<diabetiaContext> CreateMockContextThrowPhysicalEventException()
        {
            var user = new Usuario { Id = 1, Email = "test@example.com" };
            var patient = new Paciente { Id = 1, IdUsuario = user.Id };
            var @event = new CargaEvento { Id = 1, IdPaciente = patient.Id, FechaEvento = DateTime.Now, NotaLibre = "nota Test" };
            var physicalActivityEvent = new EventoActividadFisica { Id = 1, IdCargaEvento = 2, IdActividadFisica = 4, Duracion = 60 };


            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalActivityEvent });

            return mockContext;
        }


        // --------------------------------------- DeletePhysicalActivityEvent Test ---------------------------------------
        [Fact]
        public async Task DeletePhysicalActivityEventAsync_GivenValidInformation_ShouldDeleteSuccessfully()
        {
            // Arrange
            var mockContextBeforeDeletion = CreateMockContextBeforeDeletion();
            var mockContextAfterDeletion = CreateMockContextAfterDeletion();
            var repositoryEvent = new EventRepository(mockContextBeforeDeletion.Object);
            var eventId = 1;

            // Act
            await repositoryEvent.DeletePhysicalActivityEventAsync(eventId);

            // Assert
            mockContextBeforeDeletion.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            mockContextBeforeDeletion.Verify(m => m.EventoActividadFisicas.Remove(It.IsAny<EventoActividadFisica>()), Times.Once);
            mockContextBeforeDeletion.Verify(m => m.CargaEventos.Remove(It.IsAny<CargaEvento>()), Times.Once);


            var deletedEvent = await mockContextAfterDeletion.Object.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == eventId);
            Assert.Null(deletedEvent);

            var deletedPhysicalEvent = await mockContextAfterDeletion.Object.EventoActividadFisicas.FirstOrDefaultAsync(eaf => eaf.IdCargaEvento == eventId);
            Assert.Null(deletedPhysicalEvent);
        }
        private Mock<diabetiaContext> CreateMockContextBeforeDeletion()
        {
            var @event = new CargaEvento { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "Old Note" };
            var physicalEvent = new EventoActividadFisica { IdCargaEvento = @event.Id, IdActividadFisica = 1, Duracion = 60 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalEvent });

            return mockContext;
        }
        private Mock<diabetiaContext> CreateMockContextAfterDeletion()
        {
            var mockContextAfterDeletion = new Mock<diabetiaContext>();
            mockContextAfterDeletion.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
            mockContextAfterDeletion.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica>());

            return mockContextAfterDeletion;
        }

        [Fact]
        public async Task DeletePhysicalActivityEventAsync_GivenInvalidEventId_ThrowsEventNotFoundException()
        {
            // Arrange
            var mockContext = MockContextDeleteThrowEventException();

            var eventRepository = new EventRepository(mockContext.Object);

            var eventId = 1;

            // Act & Assert
            await Assert.ThrowsAsync<EventNotFoundException>(async () =>
            await eventRepository.DeletePhysicalActivityEventAsync(eventId));
        }
        private Mock<diabetiaContext> MockContextDeleteThrowEventException()
        {
            var @event = new CargaEvento { Id = 2, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "nota Test" };
            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });

            return mockContext;
        }

        [Fact]
        public async Task DeletePhysicalActivityEventAsync_GivenValidEventNotMatchPhysicalActivity_ThrowsPhysicalEventNotMatchException()
        {
            // Arrange
            var mockContext = MockContextDeleteThrowPhysicalMismatchException();

            var eventRepository = new EventRepository(mockContext.Object);

            var eventId = 1;

            // Act & Assert
            await Assert.ThrowsAsync<PhysicalEventNotMatchException>(async () =>
            await eventRepository.DeletePhysicalActivityEventAsync(eventId));
        }
        private Mock<diabetiaContext> MockContextDeleteThrowPhysicalMismatchException()
        {
            var @event = new CargaEvento { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "nota Test" };
            var physicalActivityEvent = new EventoActividadFisica { Id = 1, IdCargaEvento = 2, IdActividadFisica = 4, Duracion = 60 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalActivityEvent });

            return mockContext;
        }
    }
}