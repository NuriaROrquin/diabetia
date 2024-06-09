using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diabetia.API.Controllers;
using Diabetia.Domain.Repositories;
using FakeItEasy;



namespace Diabetia.Test._2_Core
{
    public class EventInsulineUseCaseTest
    {
        [Fact]
        public async Task AddInsulinEventWithrguments()
        {
            var FakeEventRepository = A.Fake<IEventRepository>();
            var EventInsulinUseCase = new EventInsulinUseCase(FakeEventRepository);

            string Email = "example@example.com";
            int IdKindEvent = 1;
            DateTime RventDate = DateTime.Now;
            string FreeNote = "Some note";
            int Insulin = 10;

            await EventInsulinUseCase.AddInsulinEvent(Email, IdKindEvent, RventDate, FreeNote, Insulin);

            A.CallTo(() => FakeEventRepository.AddInsulinEvent(
                Email,
                IdKindEvent,
                RventDate,
                FreeNote,
                Insulin)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task EditInsulinEventWithCorrectArguments()
        {
            var FakeEventRepository = A.Fake<IEventRepository>();
            var EventInsulinUseCase = new EventInsulinUseCase(FakeEventRepository);

            int IdEvent = 123;
            string Email = "example@example.com";
            DateTime EventDate = DateTime.Now;
            string FreeNote = "Some updated note";
            int Insulin = 20;


            await EventInsulinUseCase.EditInsulinEvent(IdEvent, Email, EventDate, FreeNote, Insulin);


            A.CallTo(() => FakeEventRepository.EditInsulinEvent(
                IdEvent,
                Email,
                EventDate,
                FreeNote,
                Insulin)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task deleteInsulinEventTest()
        {
            var FakeEventRepository = A.Fake<IEventRepository>();
            var EventInsulinUseCase = new EventInsulinUseCase(FakeEventRepository);

            int IdEvent = 123; 
            string Email = "example@example.com";

            await EventInsulinUseCase.DeleteInsulinEvent(IdEvent, Email);

            A.CallTo(() => FakeEventRepository.DeleteInsulinEvent(
                IdEvent,
                Email)).MustHaveHappenedOnceExactly();
        }
    }
}
