using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Utilities;
using FakeItEasy;
using System.Collections.Generic;
using System.Threading.Tasks;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;
using Diabetia.Domain.Services;
using Xunit;

namespace Diabetia_Core.Home
{
    public class HomeUseCaseTests
    {
        private readonly IHomeRepository _fakeHomeRepository;
        private readonly IEventRepository _fakeEventRepository;
        private readonly HomeUseCase _homeUseCase;

        public HomeUseCaseTests()
        {
            _fakeHomeRepository = A.Fake<IHomeRepository>();
            _fakeEventRepository = A.Fake<IEventRepository>();
            _homeUseCase = new HomeUseCase(_fakeHomeRepository, _fakeEventRepository);
        }

        [Fact]
        public async Task ShowMetrics_ShouldReturnMetrics()
        {
            // Arrange
            var email = "test@example.com";
            var dateFilter = new DateFilter();
            var metrics = new Metrics();

            A.CallTo(() => _fakeHomeRepository.GetPhysicalActivity(email, (int)TypeEventEnum.ACTIVIDADFISICA, dateFilter)).Returns(100);
            A.CallTo(() => _fakeHomeRepository.GetChMetrics(email, (int)TypeEventEnum.COMIDA, dateFilter)).Returns(200);
            A.CallTo(() => _fakeHomeRepository.GetGlucose(email, (int)TypeEventEnum.GLUCOSA, dateFilter)).Returns(150);
            A.CallTo(() => _fakeHomeRepository.GetHyperglycemia(email, dateFilter)).Returns(10);
            A.CallTo(() => _fakeHomeRepository.GetHypoglycemia(email, dateFilter)).Returns(5);
            A.CallTo(() => _fakeHomeRepository.GetInsulin(email, (int)TypeEventEnum.INSULINA, dateFilter)).Returns(50);

            // Act
            var result = await _homeUseCase.ShowMetrics(email, dateFilter);

            // Assert
            Assert.Equal(100, result.PhysicalActivity);
            Assert.Equal(200, result.Carbohydrates);
            Assert.Equal(150, result.Glycemia);
            Assert.Equal(10, result.Hyperglycemia);
            Assert.Equal(5, result.Hypoglycemia);
            Assert.Equal(50, result.Insulin);

            A.CallTo(() => _fakeHomeRepository.GetPhysicalActivity(email, (int)TypeEventEnum.ACTIVIDADFISICA, dateFilter)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeHomeRepository.GetChMetrics(email, (int)TypeEventEnum.COMIDA, dateFilter)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeHomeRepository.GetGlucose(email, (int)TypeEventEnum.GLUCOSA, dateFilter)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeHomeRepository.GetHyperglycemia(email, dateFilter)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeHomeRepository.GetHypoglycemia(email, dateFilter)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeHomeRepository.GetInsulin(email, (int)TypeEventEnum.INSULINA, dateFilter)).MustHaveHappenedOnceExactly();
        }

       [Fact]
        public async Task GetTimeline_ShouldReturnTimeline_WithAllEventTypes()
        {
            // Arrange
            var email = "test@example.com";
            var lastEvents = new List<CargaEvento>
            {
                new CargaEvento { Id = 1 },
                new CargaEvento { Id = 2 },
                new CargaEvento { Id = 3 },
                new CargaEvento { Id = 4 },
                new CargaEvento { Id = 5 },
                new CargaEvento { Id = 6 },
                new CargaEvento { Id = 7 }
            };

            var glucoseEvent = new GlucoseEvent { IdEventType = (int)TypeEventEnum.GLUCOSA, Title = "Glucose", GlucoseLevel = 20, DateEvent = new System.DateTime() };
            var insulinEvent = new InsulinEvent { IdEventType = (int)TypeEventEnum.INSULINA, Title = "Insulin", DateEvent = new System.DateTime() };
            var foodEvent = new FoodEvent { IdEventType = (int)TypeEventEnum.COMIDA, Title = "Food", IngredientName = "Apple", DateEvent = new System.DateTime() };
            var physicalActivityEvent = new PhysicalActivityEvent { IdEventType = (int)TypeEventEnum.ACTIVIDADFISICA, Title = "Run", Duration = 30, DateEvent = new System.DateTime() };
            var healthEvent = new HealthEvent { IdEventType = (int)TypeEventEnum.EVENTODESALUD, Title = "Health Check", DateEvent = new System.DateTime() };
            var medicalVisitEvent = new MedicalVisitEvent { IdEventType = (int)TypeEventEnum.VISITAMEDICA, Title = "Doctor Visit", DateEvent = new System.DateTime() };
            var examEvent = new ExamEvent { IdEventType = (int)TypeEventEnum.ESTUDIOS, Title = "Blood Test", DateEvent = new System.DateTime() };

            A.CallTo(() => _fakeHomeRepository.GetLastEvents(email)).Returns(lastEvents);
            A.CallTo(() => _fakeEventRepository.GetEventType(1)).Returns(TypeEventEnum.GLUCOSA);
            A.CallTo(() => _fakeEventRepository.GetEventType(2)).Returns(TypeEventEnum.INSULINA);
            A.CallTo(() => _fakeEventRepository.GetEventType(3)).Returns(TypeEventEnum.COMIDA);
            A.CallTo(() => _fakeEventRepository.GetEventType(4)).Returns(TypeEventEnum.ACTIVIDADFISICA);
            A.CallTo(() => _fakeEventRepository.GetEventType(5)).Returns(TypeEventEnum.EVENTODESALUD);
            A.CallTo(() => _fakeEventRepository.GetEventType(6)).Returns(TypeEventEnum.VISITAMEDICA);
            A.CallTo(() => _fakeEventRepository.GetEventType(7)).Returns(TypeEventEnum.ESTUDIOS);

            A.CallTo(() => _fakeEventRepository.GetGlucoseEventById(1)).Returns(glucoseEvent);
            A.CallTo(() => _fakeEventRepository.GetInsulinEventById(2)).Returns(insulinEvent);
            A.CallTo(() => _fakeEventRepository.GetFoodEventById(3)).Returns(foodEvent);
            A.CallTo(() => _fakeEventRepository.GetPhysicalActivityById(4)).Returns(physicalActivityEvent);
            A.CallTo(() => _fakeEventRepository.GetHealthEventById(5)).Returns(healthEvent);
            A.CallTo(() => _fakeEventRepository.GetMedicalVisitEventById(6)).Returns(medicalVisitEvent);
            A.CallTo(() => _fakeEventRepository.GetExamEventById(7)).Returns(examEvent);

            // Act
            var result = await _homeUseCase.GetTimeline(email);

            // Assert
            Assert.Equal(7, result.Items.Count);

            Assert.Equal("Glucose 20", result.Items[0].Title);
            Assert.Equal(glucoseEvent.DateEvent, result.Items[0].DateTime);
            Assert.True(result.Items[0].IsWarning);

            Assert.Equal("Insulin", result.Items[1].Title);
            Assert.Equal(insulinEvent.DateEvent, result.Items[1].DateTime);

            Assert.Equal("Food - Apple", result.Items[2].Title);
            Assert.Equal(foodEvent.DateEvent, result.Items[2].DateTime);

            Assert.Equal("Run 30min", result.Items[3].Title);
            Assert.Equal(physicalActivityEvent.DateEvent, result.Items[3].DateTime);

            Assert.Equal("Health Check", result.Items[4].Title);
            Assert.Equal(healthEvent.DateEvent, result.Items[4].DateTime);

            Assert.Equal("Doctor Visit", result.Items[5].Title);
            Assert.Equal(medicalVisitEvent.DateEvent, result.Items[5].DateTime);

            Assert.Equal("Blood Test", result.Items[6].Title);
            Assert.Equal(examEvent.DateEvent, result.Items[6].DateTime);

            A.CallTo(() => _fakeHomeRepository.GetLastEvents(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventType(1)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventType(2)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventType(3)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventType(4)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventType(5)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventType(6)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventType(7)).MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeEventRepository.GetGlucoseEventById(1)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetInsulinEventById(2)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetFoodEventById(3)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetPhysicalActivityById(4)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetHealthEventById(5)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetMedicalVisitEventById(6)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetExamEventById(7)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task GetTimeline_ShouldHandleEmptyEvents()
        {
            // Arrange
            var email = "test@example.com";
            var lastEvents = new List<CargaEvento>();

            A.CallTo(() => _fakeHomeRepository.GetLastEvents(email)).Returns(lastEvents);

            // Act
            var result = await _homeUseCase.GetTimeline(email);

            // Assert
            Assert.Empty(result.Items);

            A.CallTo(() => _fakeHomeRepository.GetLastEvents(email)).MustHaveHappenedOnceExactly();
        }

    }
}
