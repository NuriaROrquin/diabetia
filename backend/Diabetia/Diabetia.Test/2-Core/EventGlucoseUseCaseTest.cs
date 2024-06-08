using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Repositories;
using FakeItEasy;
using Xunit;

namespace Diabetia.Test._2_Core
{
    public class EventGlucoseUseCaseTest
    {
        [Fact]
        public async Task AddGlucoseEventTest()
        {
            var FakeEventRepository = A.Fake<IEventRepository>();
            var EventGlucoseUseCase = new EventGlucoseUseCase(FakeEventRepository);

            string Email = "example@example.com";
            int KindEvent = 1;
            DateTime EventDate = DateTime.Now;
            string FreeNote = "Some note";
            decimal Glucose = 5;
            int? IdDevicePacient = 123;
            int? IdFoodEvent = 456;
            bool? PostFoodMedition = true;

            await EventGlucoseUseCase.AddGlucoseEvent(Email, KindEvent, EventDate, FreeNote, Glucose, IdDevicePacient, IdFoodEvent, PostFoodMedition);

            A.CallTo(() => FakeEventRepository.AddGlucoseEvent(
                Email,
                KindEvent,
                EventDate,
                FreeNote,
                Glucose,
                IdDevicePacient,
                IdFoodEvent,
                PostFoodMedition)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditGlucoseEventWithCorrectArgumentsTest()
        {
            var FakeEventRepository = A.Fake<IEventRepository>();
            var EventGlucoseUseCase = new EventGlucoseUseCase(FakeEventRepository);

            int IdEvent = 12; 
            string Email = "example@example.com";
            DateTime EventDate = DateTime.Now;
            string FreeNote = "Updated note";
            decimal Glucose = 7;
            int? IdDevicePacient = null; 
            int? IdFoodEvent = null; 
            bool? PostFoodMedition = false;

            await EventGlucoseUseCase.EditGlucoseEvent(IdEvent, Email, EventDate, FreeNote, Glucose, IdDevicePacient, IdFoodEvent, PostFoodMedition);

            // Assert
            A.CallTo(() => FakeEventRepository.EditGlucoseEvent(
                IdEvent,
                Email,
                EventDate,
                FreeNote,
                Glucose,
                IdDevicePacient,
                IdFoodEvent,
                PostFoodMedition)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteGlucoseEventTest()
        {
            var FakeEventRepository = A.Fake<IEventRepository>();
            var EventGlucoseUseCase = new EventGlucoseUseCase(FakeEventRepository);

            int IdEvent = 12; 
            string Email = "example@example.com";

            await EventGlucoseUseCase.DeleteGlucoseEvent(IdEvent, Email);

            A.CallTo(() => FakeEventRepository.DeleteGlucoseEvent(
                IdEvent,
                Email)).MustHaveHappenedOnceExactly();
        }
    }
}
