using Diabetia.Application.UseCases;
using Diabetia.Domain.Repositories;
using FakeItEasy;

namespace Diabetia.Test._2_Core.EventUseCases
{
    public class PhysicalEventUseCaseTest
    {
        [Fact]
        public async Task EventPhysicalActivityUseCase_WhenCalledWithValidData_ShouldEditEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var eventId = 1;
            var eventDate = DateTime.Now.AddDays(1);
            var physicalActivity = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var freeNote = "Test note";
            var fakeEventRepository = A.Fake<IEventRepository>();

            var fakeEventPhysicalActivityUseCase = new EventPhysicalActivityUseCase(fakeEventRepository);

            await fakeEventPhysicalActivityUseCase.EditPhysicalEvent(email, eventId, eventDate, physicalActivity,iniciateTime, finishTime, freeNote);

            // Act & Assert 
            A.CallTo(() => fakeEventRepository.EditPhysicalActivityEvent(email, eventId, eventDate, physicalActivity, iniciateTime,finishTime,freeNote)).MustHaveHappenedOnceExactly();
        }
    }
}
