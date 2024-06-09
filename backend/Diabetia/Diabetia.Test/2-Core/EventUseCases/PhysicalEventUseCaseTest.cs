using Diabetia.Application.UseCases;
using Diabetia.Domain.Repositories;
using FakeItEasy;

namespace Diabetia.Test._2_Core.EventUseCases
{
    public class PhysicalEventUseCaseTest
    {
        [Fact]
        public async Task EventPhysicalActivityUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var kindEventId = 1;
            var eventDate = DateTime.Now.AddDays(1);
            var freeNote = "Test note";
            var physicalActivityId = 1;
            var iniciateTime = new TimeSpan(10, 0, 0);
            var finishTime = new TimeSpan(11, 0, 0);
            var fakeEventRepository = A.Fake<IEventRepository>();

            var fakeEventPhysicalActivityUseCase = new EventPhysicalActivityUseCase(fakeEventRepository);

            await fakeEventPhysicalActivityUseCase.AddPhysicalEventAsync(email, kindEventId, eventDate, freeNote, physicalActivityId, iniciateTime, finishTime);

            // Act & Assert 
            A.CallTo(() => fakeEventRepository.AddPhysicalActivityEventAsync(email, kindEventId, eventDate, freeNote, physicalActivityId, iniciateTime, finishTime)).MustHaveHappenedOnceExactly();
        }

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

            await fakeEventPhysicalActivityUseCase.EditPhysicalEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote);

            // Act & Assert 
            A.CallTo(() => fakeEventRepository.EditPhysicalActivityEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EventPhysicalActivityUseCase_WhenCalledWithValidData_ShouldDeleteEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var eventId = 1;
           
            var fakeEventRepository = A.Fake<IEventRepository>();

            var fakeEventPhysicalActivityUseCase = new EventPhysicalActivityUseCase(fakeEventRepository);

            await fakeEventPhysicalActivityUseCase.DeletePhysicalEventAsync(email, eventId);

            // Act & Assert 
            A.CallTo(() => fakeEventRepository.DeletePhysicalActivityEventAsync(email, eventId)).MustHaveHappenedOnceExactly();
        }
    }
}
