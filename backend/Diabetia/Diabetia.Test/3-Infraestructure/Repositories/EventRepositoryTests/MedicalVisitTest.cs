using Moq;
using Diabetia.Infrastructure.Repositories;
using Diabetia.Domain.Models;
using Moq.EntityFrameworkCore;
using Diabetia.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Diabetia.Infraestructure.EF;
using Diabetia.API;

namespace Diabetia.Test._3_Infraestructure.Repositories.EventRepositoryTests
{
    public class MedicalVisitTest
    {
        // --------------------------------------- AddMedicalVisitEvent Test ---------------------------------------
        [Fact]
        public async Task AddMedicalVisitEventAsync_GivenValidDataAndRecordatory_ShouldAddEventAndMedicalVisit()
        {
            // Arrange
            var mockContext = CreateMockContextAddMedicalVisitWithRecordatoryEvent();

            var repository = new EventRepository(mockContext.Object);

            var email = "test@example.com";
            var kindEventId = 6;
            var visitDate = DateTime.Now.AddDays(1);
            var professionalId = 1;
            var recordatory = true;
            var recordatoryDate = DateTime.Now.AddDays(2);
            var description = "Test agregar visita medica Pablo";

            // Act
            await repository.AddMedicalVisitEventAsync(email, kindEventId, visitDate, professionalId, recordatory, recordatoryDate, description);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(4));
            mockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoVisitaMedicas.Add(It.IsAny<EventoVisitaMedica>()), Times.Once);
            mockContext.Verify(m => m.Recordatorios.Add(It.IsAny<Recordatorio>()), Times.Once);
            mockContext.Verify(m => m.RecordatorioEventos.Add(It.IsAny<RecordatorioEvento>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextAddMedicalVisitWithRecordatoryEvent()
        {
            var user = new Usuario { Id = 1, Email = "test@example.com" };
            var patient = new Paciente { Id = 1, IdUsuario = user.Id };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
            mockContext.Setup(m => m.EventoVisitaMedicas).ReturnsDbSet(new List<EventoVisitaMedica>());
            mockContext.Setup(m => m.Recordatorios).ReturnsDbSet(new List<Recordatorio>());
            mockContext.Setup(m => m.RecordatorioEventos).ReturnsDbSet(new List<RecordatorioEvento>());

            return mockContext;
        }

        [Fact]
        public async Task AddMedicalVisitEventAsync_GivenValidDataWithoutRecordatory_ShouldAddEventAndMedicalVisit()
        {
            // Arrange
            var mockContext = CreateMockContextAddMedicalVisitWithoutRecordatoryEvent();

            var repository = new EventRepository(mockContext.Object);

            var email = "test@example.com";
            var kindEventId = 6;
            var visitDate = DateTime.Now.AddDays(1);
            var professionalId = 1;
            var recordatory = false;
            var recordatoryDate = DateTime.Now.AddDays(2);
            var description = "Test agregar visita medica Pablo";

            // Act
            await repository.AddMedicalVisitEventAsync(email, kindEventId, visitDate, professionalId, recordatory, recordatoryDate, description);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            mockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoVisitaMedicas.Add(It.IsAny<EventoVisitaMedica>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextAddMedicalVisitWithoutRecordatoryEvent()
        {
            var user = new Usuario { Id = 1, Email = "test@example.com" };
            var patient = new Paciente { Id = 1, IdUsuario = user.Id };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
            mockContext.Setup(m => m.EventoVisitaMedicas).ReturnsDbSet(new List<EventoVisitaMedica>());

            return mockContext;
        }

        [Fact]
        public async Task AddMedicalVisitEventAsync_GivenNotRegisteredEmail_ThrowsUserNotFoundOnDBException()
        {
            // Arrange
            var mockContext = CreateMockContextAddMedicalVisitThrowsUserNotFound();

            var repository = new EventRepository(mockContext.Object);

            var email = "testInvalid@gmail.com";
            var kindEventId = 6;
            var visitDate = DateTime.Now.AddDays(1);
            var professionalId = 1;
            var recordatory = true;
            var recordatoryDate = DateTime.Now.AddDays(2);
            var description = "Test agregar visita medica Pablo";

            // Act / Assert
            await Assert.ThrowsAsync<UserNotFoundOnDBException>(async () =>
            await repository.AddMedicalVisitEventAsync(email, kindEventId, visitDate, professionalId, recordatory, recordatoryDate, description));

        }
        private Mock<diabetiaContext> CreateMockContextAddMedicalVisitThrowsUserNotFound()
        {
            var user = new Usuario { Id = 1, Email = "test@gmail.com" };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });

            return mockContext;
        }

        [Fact]
        public async Task AddMedicalVisitEventAsync_GivenNotPatientUser_ThrowsPatientNotFoundException()
        {
            // Arrange
            var mockContext = CreateMockContextAddMedicalVisitThrowsPatientNotFound();

            var repository = new EventRepository(mockContext.Object);

            var email = "test@gmail.com";
            var kindEventId = 6;
            var visitDate = DateTime.Now.AddDays(1);
            var professionalId = 1;
            var recordatory = true;
            var recordatoryDate = DateTime.Now.AddDays(2);
            var description = "Test agregar visita medica Pablo";

            // Act / Assert
            await Assert.ThrowsAsync<PatientNotFoundException>(async () =>
            await repository.AddMedicalVisitEventAsync(email, kindEventId, visitDate, professionalId, recordatory, recordatoryDate, description));

        }
        private Mock<diabetiaContext> CreateMockContextAddMedicalVisitThrowsPatientNotFound()
        {
            var user = new Usuario { Id = 1, Email = "test@gmail.com" };
            var patient = new Paciente { Id = 1, IdUsuario = 2 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });

            return mockContext;
        }

        
    }
}
