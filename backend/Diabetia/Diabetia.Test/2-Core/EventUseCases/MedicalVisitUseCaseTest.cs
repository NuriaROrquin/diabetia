using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Repositories;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Test._2_Core.EventUseCases
{
    public class MedicalVisitUseCaseTest
    {
        [Fact]
        public async Task EventMedicalVisitUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var kindEventId = 1;
            var eventDate = DateTime.Now.AddDays(1);
            var professionalId = 1;
            var recordatory = true;
            var recordatoryDate = DateTime.Now.AddDays(2);
            var description = "Test useCase";
            var fakeEventRepository = A.Fake<IEventRepository>();

            var fakeEventMedicalVisitUseCase = new EventMedicalVisitUseCase(fakeEventRepository);

            await fakeEventMedicalVisitUseCase.AddMedicalVisitEventAsync(email, kindEventId, eventDate, professionalId, recordatory, recordatoryDate, description);

            // Act & Assert 
            A.CallTo(() => fakeEventRepository.AddMedicalVisitEventAsync(email,kindEventId,eventDate,professionalId,recordatory,recordatoryDate,description)).MustHaveHappenedOnceExactly();
        }
    }
}
