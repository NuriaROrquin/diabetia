using Moq;
using Diabetia.Infrastructure.Repositories;
using Diabetia.Domain.Models;
using Moq.EntityFrameworkCore;
using Diabetia.Domain.Exceptions;
using Diabetia.Infrastructure.EF;

namespace Diabetia.Test._3_Infraestructure.Repositories.EventRepositoryTests
{
    public class MedicalVisitTest
    {
        // --------------------------------------- ⬇⬇ AddMedicalVisitEvent Test ⬇⬇ ---------------------------------------
        [Fact]
        public async Task AddMedicalVisitEventAsync_GivenValidData_ShouldAddEventAndMedicalVisit()
        {
            // Arrange
            var mockContext = CreateMockContextAddMedicalVisit();
            var repository = new EventRepository(mockContext.Object);

            int patientId = 1;
            var medicalVisit = new EventoVisitaMedica()
            {
                IdProfesional = 1,
                Descripcion = "Test description",
                IdCargaEventoNavigation = new CargaEvento()
                {
                    Id = 1,
                    IdTipoEvento = 6,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test description",
                    EsNotaLibre = false,
                }
            };

            // Act
            await repository.AddMedicalVisitEventAsync(patientId, medicalVisit);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            mockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoVisitaMedicas.Add(It.IsAny<EventoVisitaMedica>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextAddMedicalVisit()
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

        // --------------------------------------- ⬇⬇ EditMedicalVisitEvent Test ⬇⬇ --------------------------------------
        [Fact]
        public async Task EditMedicalVisitEventAsync_GivenValidData_ShouldEditEventandMedicalVisitSuccessfully()
        {
            // Arrange
            var mockContext = CreateMockContextEditMedicalVisit();
            var fakeRepository = new EventRepository(mockContext.Object);
            var medicalVisit = new EventoVisitaMedica()
            {
                IdProfesional = 1,
                Descripcion = "Test description",
                IdCargaEventoNavigation = new CargaEvento()
                {
                    Id = 1,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test description",
                    FueRealizado = false
                }
            };

            // Act
            await fakeRepository.EditMedicalVisitEventAsync(medicalVisit);

            // Assert
            var @event = mockContext.Object.CargaEventos.First();
            var medicalVisitEvent = mockContext.Object.EventoVisitaMedicas.First();

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.CargaEventos.Update(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoVisitaMedicas.Update(It.IsAny<EventoVisitaMedica>()), Times.Once);

            Assert.Equal(medicalVisitEvent.IdCargaEventoNavigation.FechaEvento, @event.FechaEvento);
            Assert.Equal(medicalVisitEvent.IdCargaEventoNavigation.NotaLibre, @event.NotaLibre);
            Assert.Equal("Test description", medicalVisitEvent.Descripcion);
        }
        private Mock<diabetiaContext> CreateMockContextEditMedicalVisit()
        {
            var @event = new CargaEvento 
            { 
                Id = 1, 
                IdPaciente = 1, 
                FechaEvento = DateTime.Now, 
                NotaLibre = "Test Note", 
                FueRealizado = false 
            };
            var medicalVisitEvent = new EventoVisitaMedica 
            { 
                IdCargaEvento = @event.Id, 
                IdProfesional = 1, 
                Descripcion = "Test Visita",
                IdCargaEventoNavigation = @event
            };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoVisitaMedicas).ReturnsDbSet(new List<EventoVisitaMedica> { medicalVisitEvent });

            return mockContext;
        }

        [Fact]
        public async Task EditMedicalVisitEventAsync_GivenInvalidEvent_ThrowsEventNotMatchException()
        {
            // Arrange
            var mockContext = CreateMockContextEditFailMedicalVisit();
            var fakeRepository = new EventRepository(mockContext.Object);
            var medicalVisit = new EventoVisitaMedica()
            {
                IdProfesional = 1,
                Descripcion = "Test description",
                IdCargaEventoNavigation = new CargaEvento()
                {
                    Id = 1,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test description",
                    FueRealizado = false
                }
            };

            // Act & Assert
            await Assert.ThrowsAsync<EventNotMatchException>(async () => 
            await fakeRepository.EditMedicalVisitEventAsync(medicalVisit));
        }
        private Mock<diabetiaContext> CreateMockContextEditFailMedicalVisit()
        {
            var @event = new CargaEvento
            {
                Id = 1,
                IdPaciente = 1,
                FechaEvento = DateTime.Now,
                NotaLibre = "Test Note",
                FueRealizado = false
            };
            var medicalVisitEvent = new EventoVisitaMedica
            {
                Id = 1,
                IdCargaEvento = 2,
                IdProfesional = 1,
                Descripcion = "Test Visita",
                IdCargaEventoNavigation = @event
            };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoVisitaMedicas).ReturnsDbSet(new List<EventoVisitaMedica> { medicalVisitEvent });

            return mockContext;
        }
    }
}