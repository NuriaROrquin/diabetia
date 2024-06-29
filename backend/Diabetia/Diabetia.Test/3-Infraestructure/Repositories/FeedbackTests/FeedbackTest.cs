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
            var foodEvent = new EventoComidum() { Id = 1, IdCargaEvento = @event.Id, Carbohidratos = 80};

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
            mockContext.Setup(m => m.EventoComida).ReturnsDbSet(new List<EventoComidum> { foodEvent });

            return mockContext;
        }


        //[Fact]
        //public async Task GetAllEventsWithoutFeedback_ReturnsCombinedEvents()
        //{
        //    // Arrange
        //    int patientId = 1;
        //    var fakeRepository = A.Fake<IFeedbackRepository>();

        //    A.CallTo(() => fakeRepository.GetFoodWithoutFeedback(patientId))
        //        .Returns(Task.FromResult(new List<FoodSummary>
        //        {
        //        new FoodSummary
        //        {
        //            EventId = 1,
        //            KindEventId = 1,
        //            EventDate = DateTime.Now,
        //            Carbohydrates = 30
        //        }
        //        }));

        //    A.CallTo(() => fakeRepository.GetPhysicalActivityWithoutFeedback(patientId))
        //        .Returns(Task.FromResult(new List<PhysicalActivitySummary>
        //        {
        //        new PhysicalActivitySummary
        //        {
        //            EventId = 2,
        //            KindEventId = 2,
        //            EventDate = DateTime.Now.AddDays(-1),
        //            ActivityName = "Running"
        //        }
        //        }));

        //    var repository = new FeedbackRepository(fakeRepository);

        //    // Act
        //    var result = await repository.GetAllEventsWithoutFeedback(patientId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(2, result.Count);

        //    var firstEvent = result.FirstOrDefault(e => e["Title"].ToString() == "Comida");
        //    Assert.NotNull(firstEvent);
        //    Assert.Equal(1, firstEvent["IdEvent"]);
        //    Assert.Equal(1, firstEvent["KindEventId"]);
        //    Assert.Equal(30, firstEvent["Carbohydrates"]);

        //    var secondEvent = result.FirstOrDefault(e => e["Title"].ToString() == "Actividad Física");
        //    Assert.NotNull(secondEvent);
        //    Assert.Equal(2, secondEvent["IdEvent"]);
        //    Assert.Equal(2, secondEvent["KindEventId"]);
        //    Assert.Equal("Running", secondEvent["ActivityName"]);
        //}
    }
}
