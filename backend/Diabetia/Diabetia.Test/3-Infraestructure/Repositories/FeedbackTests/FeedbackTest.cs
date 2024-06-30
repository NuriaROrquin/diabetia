using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using Moq;
using Moq.EntityFrameworkCore;

namespace Diabetia_Infrastructure.Repositories.Feedback
{

    public class FeedbackTest
    {
        public FeedbackTest() { }

        // -------------------------------------------------- ⬇⬇ Physical Activity Test ⬇⬇ ---------------------------------------------------------
        [Fact]
        public async Task GetPhysicalActivityWithoutFeedback_GivenPatientId_ShouldGetEventsSuccessfully()
        {
            //Arrange
            var mockContext = CreateMockContextGetPhysicalActivities();
            var fakeRepository = new FeedbackRepository(mockContext.Object);

            int patientId = 1;

            //Act
            var result = await fakeRepository.GetPhysicalActivityWithoutFeedback(patientId);

            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("TestActividad", result.First().ActivityName);
        }

        private Mock<diabetiaContext> CreateMockContextGetPhysicalActivities()
        {
            var patient = new Paciente() { Id = 1 };
            var @event = new CargaEvento() { Id = 1, IdPaciente = patient.Id, NotaLibre = "Test Nota", EsNotaLibre = true, FueRealizado = false };
            var physicalActivityEvent = new EventoActividadFisica() { Id = 1, IdCargaEvento = @event.Id, IdActividadFisica = 1 };
            var activity = new ActividadFisica() { Id = 1, Nombre = "TestActividad" };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalActivityEvent });
            mockContext.Setup(m => m.ActividadFisicas).ReturnsDbSet(new List<ActividadFisica> { activity });

            return mockContext;
        }


        // -------------------------------------------------- ⬇⬇ Food Test ⬇⬇ ---------------------------------------------------------
        [Fact]
        public async Task GetFoodWithoutFeedback_GivenPatientId_ShouldGetEventsSuccessfully()
        {
            //Arrange
            var mockContext = CreateMockContextGetFood();
            var fakeRepository = new FeedbackRepository(mockContext.Object);

            int patientId = 1;

            //Act
            var result = await fakeRepository.GetFoodWithoutFeedback(patientId);

            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(80, result.First().Carbohydrates);
        }

        private Mock<diabetiaContext> CreateMockContextGetFood()
        {
            var patient = new Paciente() { Id = 1 };
            var @event = new CargaEvento() { Id = 1, IdPaciente = patient.Id, NotaLibre = "Test Nota", EsNotaLibre = true, FueRealizado = false };
            var foodEvent = new EventoComidum() { Id = 1, IdCargaEvento = @event.Id, Carbohidratos = 80 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoComida).ReturnsDbSet(new List<EventoComidum> { foodEvent });

            return mockContext;
        }


        [Fact]
        public async Task GetAllEventsWithoutFeedback_ReturnsCombinedEvents()
        {
            // Arrange
            int patientId = 1;
            var mockContext = CreateMockContextGetPhysicalActivitiesDos();
            var fakeRepository = new FeedbackRepository(mockContext.Object);

            //Act
            var result = await fakeRepository.GetAllEventsWithoutFeedback(patientId);

            Assert.NotNull(mockContext);
        }

        private Mock<diabetiaContext> CreateMockContextGetPhysicalActivitiesDos()
        {
            var patient = new Paciente() { Id = 1 };
            var @event = new CargaEvento() { Id = 1, IdPaciente = patient.Id, NotaLibre = "Test Nota", EsNotaLibre = true, FueRealizado = false };
            var physicalActivityEvent = new EventoActividadFisica() { Id = 1, IdCargaEvento = @event.Id, IdActividadFisica = 1 };
            var activity = new ActividadFisica() { Id = 1, Nombre = "TestActividad" };
            var foodEvent = new EventoComidum() { Id = 1, IdCargaEvento = @event.Id, Carbohidratos = 80 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalActivityEvent });
            mockContext.Setup(m => m.ActividadFisicas).ReturnsDbSet(new List<ActividadFisica> { activity });
            mockContext.Setup(m => m.EventoComida).ReturnsDbSet(new List<EventoComidum> { foodEvent });

            return mockContext;
        }

        [Fact]
        public async Task AddFeedback_GivenFeedback_ShouldAddSuccessfully()
        {
            // Arrange
            var feedback = new Diabetia.Domain.Models.Feedback()
            {
                IdSentimiento = 1,
                FueRealizado = true,
                NotaLibre = "Testing",
                IdCargaEventoNavigation = new CargaEvento
                {
                    Id=1,
                    FueRealizado = true
                }
            };
            var mockContext = CreateMockContextAddFeedback();
            var feedbackRepository = new FeedbackRepository(mockContext.Object);

            //Act
            await feedbackRepository.AddFeedback(feedback);

            //Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(m => m.CargaEventos.Update(It.IsAny<CargaEvento>()), Times.Once);
            mockContext.Verify(m => m.Feedbacks.Add(It.IsAny<Diabetia.Domain.Models.Feedback>()), Times.Once);
        }

        private Mock<diabetiaContext> CreateMockContextAddFeedback()
        {
            var @event = new CargaEvento() { Id = 1 };
            var feedback = new Diabetia.Domain.Models.Feedback() { Id = 1, IdCargaEvento = @event.Id, IdSentimiento = 1, FueRealizado = true };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.Feedbacks).ReturnsDbSet(new List<Diabetia.Domain.Models.Feedback> { feedback });

            return mockContext;
        }
    }
}
