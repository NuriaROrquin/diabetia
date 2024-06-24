using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;


namespace Diabetia.Test._3_Infraestructure.Repositories.EventTests
{
    public class FoodTest
    {/*
        [Fact]
        public async Task AddFoodEventAsync_GivenValidData_ShouldAddAFoodEvent()
        {
            var mockContext = CreateMockContextAddPassCorrect();

            var repository = new EventRepository(mockContext.Object);

            var food = new EventoComidum()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 2,
                    FechaEvento = DateTime.Now.AddDays(1),
                    NotaLibre = "Test Note",
                    FechaActual = DateTime.Now,
                    FueRealizado = false,
                    EsNotaLibre = false
                },
                IngredienteComida = new List<IngredienteComidum>
                {
                    new IngredienteComidum
                    {
                        IdIngrediente = 1,
                        CantidadIngerida = 100
                    }
                }
            };
            int patientId = 11;

            await repository.AddFoodEventAsync(patientId, food);

            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
            mockContext.Verify(m => m.CargaEventos.Add(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.EventoComida.Add(It.IsAny<EventoComidum>()), Times.Once);
        }
        private Mock<diabetiaContext> CreateMockContextAddPassCorrect()
        {
            var patient = new Paciente { Id = 1 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento>());
            mockContext.Setup(m => m.EventoComida).ReturnsDbSet(new List<EventoComidum>());

            return mockContext;
        }*/
    }
}
