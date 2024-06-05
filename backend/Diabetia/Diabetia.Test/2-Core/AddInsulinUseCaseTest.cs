using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diabetia.Domain.Repositories;
using Xunit;
using FakeItEasy;
using Diabetia.Application.UseCases;
using Diabetia.API.Controllers;

namespace Diabetia.Test._2_Core
{
    public class AddInsulinUseCaseTest
    {
        [Fact]
        public async Task AddInsulinEvent_ShouldCallRepositoryWithCorrectParameters()
        {
            // Arrange
            var fakeRepository = A.Fake<IEventRepository>();
            var useCase = new AddInsulinEventUseCase(fakeRepository);

            var email = "test@example.com";
            var idKindEvent = 1;
            var eventDate = DateTime.Now;
            var freeNote = "Test note";
            var insulin = 10;

            // Act
            await useCase.AddInsulinEvent(email, idKindEvent, eventDate, freeNote, insulin);

            // Assert
            A.CallTo(() => fakeRepository.AddInsulinEvent(email, idKindEvent, eventDate, freeNote, insulin))
                .MustHaveHappenedOnceExactly();
        }
    }
}

