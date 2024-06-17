using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Exceptions;
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
            var email = "emailTest@example.com";
            var physicalActivityEvent = new EventoActividadFisica();
            var patient = new Paciente()
            {
                Id = 1
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeEventPhysicalActivityUseCase = new PhysicalActivityUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakeUserRepository.GetPatient(email)).Returns(patient);
            await fakeEventPhysicalActivityUseCase.AddPhysicalEventAsync(email, physicalActivityEvent);

            // Act & Assert 
            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.AddPhysicalActivityEventAsync(patient.Id, physicalActivityEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EventPhysicalActivityUseCase_WhenCalledInvalidPatient_ThrowsPatientNotFoundException()
        {
            var email = "emailTest@example.com";
            var physicalActivityEvent = new EventoActividadFisica();
            var patient = new Paciente()
            {
                Id = 1
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeEventPhysicalActivityUseCase = new PhysicalActivityUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();


            // Act & Assert 
            await Assert.ThrowsAsync<PatientNotFoundException>(() => fakeEventPhysicalActivityUseCase.AddPhysicalEventAsync(email, physicalActivityEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }
    }
}



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
