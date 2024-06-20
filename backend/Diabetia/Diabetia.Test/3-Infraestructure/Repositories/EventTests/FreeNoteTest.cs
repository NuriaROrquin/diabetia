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

        // --------------------------------------- ⬇⬇ EditFreeNoteEvent Test ⬇⬇ ---------------------------------------
        [Fact]
        public async Task EditFreeNoteEvent_GivenValidData_ShouldUpdateEventSuccessfully()
        {
            // Arrange
            var mockContext = CreateMockContextEditPassCorrect();

            var fakeRepository = new EventRepository(mockContext.Object);

            var freeNoteEvent = new CargaEvento()
            {
                Id = 1,
                NotaLibre = "Test",
                EsNotaLibre = true,
                FechaEvento = DateTime.Now.AddDays(1),
                FueRealizado = false
            };

            // Act
            await fakeRepository.EditFreeNoteEventAsync(freeNoteEvent);

            // Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            var @event = mockContext.Object.CargaEventos.First();
            Assert.Equal("Test", freeNoteEvent.NotaLibre);
        }
        private Mock<diabetiaContext> CreateMockContextEditPassCorrect()
        {
            var @event = new CargaEvento { Id = 1, IdPaciente = 1, FechaEvento = DateTime.Now, NotaLibre = "Test Note", EsNotaLibre = true };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });

            return mockContext;
        }
    }
}
