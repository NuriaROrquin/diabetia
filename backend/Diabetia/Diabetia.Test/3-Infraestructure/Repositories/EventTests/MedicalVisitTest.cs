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
        // --------------------------------------- AddMedicalVisitEvent Test ---------------------------------------
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
            //mockContext.Verify(m => m.Recordatorios.Add(It.IsAny<Recordatorio>()), Times.Once);
            //mockContext.Verify(m => m.RecordatorioEventos.Add(It.IsAny<RecordatorioEvento>()), Times.Once);
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

        //        // --------------------------------------- AddMedicalVisitEvent Test --------------------------------------
    }
}