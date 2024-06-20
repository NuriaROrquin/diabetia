using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

namespace Diabetia.Test._3_Infraestructure.Repositories.EventTests
{
    public class FreeNoteTest
    {
        // --------------------------------------- ⬇⬇ AddFreeNoteEvent Test ⬇⬇ ---------------------------------------
        [Fact]
        public async Task AddFreeNoteEventAsync_GivenValidData_ShouldAddEventSuccessfully()
        {
            // Arrange
            var mockContext = CreateMockContextAddPassCorrect();
            var repository = new EventRepository(mockContext.Object);
            var freeNoteEvent = new CargaEvento()
            {
                IdTipoEvento = 8,
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = true
            };
            int patientId = 1;

            // Act
            await repository.AddFreeNoteEventAsync(patientId, freeNoteEvent);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextAddPassCorrect()
        {
            var patient = new Paciente () { Id = 1 };
            var @event = new CargaEvento() { Id = 1, IdPaciente = patient.Id, NotaLibre = "Test Nota", EsNotaLibre = true };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });

            return mockContext;
        }

    }
}
