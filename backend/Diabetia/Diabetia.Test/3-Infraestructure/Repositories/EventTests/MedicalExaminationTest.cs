using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

namespace Diabetia.Test._3_Infraestructure.Repositories.EventTests
{
    public class MedicalExaminationTest
    {
        // --------------------------------------- ⬇⬇ Add MedicalExaminationEvent Test ⬇⬇ ---------------------------------------
        [Fact]
        public async Task AddMedicalExaminationEventAsync_GivenValidData_ShouldAddEventAndMedicalExaminationSuccessfully()
        {
            //Arrange
            var fakeMockContext = CreateMockContextAddMedicalExamination();
            var fakeRepository = new EventRepository(fakeMockContext.Object);

            int fakePatientId = 1;
            string fakeFileSaved = "TestFileId";
            var medicalExamination = new EventoEstudio()
            {
                IdProfesional = 1,
                TipoEstudio = "Test",
                Archivo = "Test",
                IdCargaEventoNavigation = new CargaEvento()
                {
                    Id = 1,
                    IdPaciente = 1,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test description",
                    EsNotaLibre = false,
                }
            };

            // Act
            await fakeRepository.AddMedicalExaminationEventAsync(fakePatientId, medicalExamination, fakeFileSaved);

            // Assert
            fakeMockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            fakeMockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
            fakeMockContext.Verify(m => m.EventoEstudios.Add(It.IsAny<EventoEstudio>()), Times.Once);

        }

        private Mock<diabetiaContext> CreateMockContextAddMedicalExamination()
        {
            var patient = new Paciente() { Id = 1, IdUsuario = 1 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
            mockContext.Setup(m => m.EventoEstudios).ReturnsDbSet(new List<EventoEstudio>());
            return mockContext;
        }

        // --------------------------------------- ⬇⬇ Delete MedicalExaminationEvent Test ⬇⬇ ---------------------------------------
    }
}
