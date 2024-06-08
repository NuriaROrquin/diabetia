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
            var fakeEventRepository = A.Fake<IEventRepository>();
            var eventInsulinUseCase = new EventInsulinUseCase(fakeEventRepository);

            int idEvent = 123;
            string email = "example@example.com";
            DateTime eventDate = DateTime.Now;
            string freeNote = "Some updated note";
            int insulin = 20;


            await eventInsulinUseCase.EditInsulinEvent(idEvent, email, eventDate, freeNote, insulin);


            A.CallTo(() => fakeEventRepository.EditInsulinEvent(
                idEvent,
                email,
                eventDate,
                freeNote,
                insulin)).MustHaveHappenedOnceExactly();
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
