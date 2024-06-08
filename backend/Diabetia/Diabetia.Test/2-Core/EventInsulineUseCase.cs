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
    public class EventInsulineUseCase
    {
        [Fact]
        public async Task AddInsulinEvent_Calls_AddInsulinEvent_On_EventRepository_With_Correct_Arguments()
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
    }
}
