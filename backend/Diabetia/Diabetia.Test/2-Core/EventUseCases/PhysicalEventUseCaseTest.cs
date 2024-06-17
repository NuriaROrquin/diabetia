using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.Events
{
    public class PhysicalEventUseCaseTest
    {
        [Fact]
        public async Task EventPhysicalActivityUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var email = "emailTest@example.com";
            var physicalActivityEvent = new EventoActividadFisica();
            var patient = new Paciente()
            {
                Id = 1
            };

            var fakeEventPhysicalActivityUseCase = new PhysicalActivityUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakeUserRepository.GetPatient(email)).Returns(patient);
            await fakeEventPhysicalActivityUseCase.AddPhysicalEventAsync(email, kindEventId, eventDate, freeNote, physicalActivityId, iniciateTime, finishTime);

            // Act & Assert 
            A.CallTo(() => fakeEventRepository.AddPhysicalActivityEventAsync(email, kindEventId, eventDate, freeNote, physicalActivityId, iniciateTime, finishTime)).MustHaveHappenedOnceExactly();
        }
    }
}

//        [Fact]
//        public async Task EventPhysicalActivityUseCase_WhenCalledWithValidData_ShouldEditEventSuccessfully()
//        {
//            var email = "emailTest@example.com";
//            var eventId = 1;
//            var eventDate = DateTime.Now.AddDays(1);
//            var physicalActivity = 1;
//            var iniciateTime = new TimeSpan(10, 0, 0);
//            var finishTime = new TimeSpan(11, 0, 0);
//            var freeNote = "Test note";
//            var fakeEventRepository = A.Fake<IEventRepository>();

//            var fakeEventPhysicalActivityUseCase = new EventPhysicalActivityUseCase(fakeEventRepository);

//            await fakeEventPhysicalActivityUseCase.EditPhysicalEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote);

//            // Act & Assert 
//            A.CallTo(() => fakeEventRepository.EditPhysicalActivityEventAsync(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote)).MustHaveHappenedOnceExactly();
//        }

//        //[Fact]
//        //public async Task EventPhysicalActivityUseCase_WhenCalledWithValidData_ShouldDeleteEventSuccessfully()
//        //{
//        //    var email = "emailTest@example.com";
//        //    var eventId = 1;

//        //    var fakeEventRepository = A.Fake<IEventRepository>();

//        //    var fakeEventPhysicalActivityUseCase = new EventPhysicalActivityUseCase(fakeEventRepository);

//        //    await fakeEventPhysicalActivityUseCase.DeletePhysicalEventAsync(email, eventId);

//        //    // Act & Assert 
//        //    A.CallTo(() => fakeEventRepository.DeletePhysicalActivityEventAsync(email, eventId)).MustHaveHappenedOnceExactly();
//        //}
//    }
//}
