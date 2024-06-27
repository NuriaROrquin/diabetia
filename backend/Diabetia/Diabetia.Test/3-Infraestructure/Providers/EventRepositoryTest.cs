using Diabetia.API.Controllers;
using Diabetia.Domain.Repositories;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Test._3_Infraestructure.Providers
{
    public class EventRepositoryTest
    {
        /*
        [Fact]
        public async Task AddInsulinEvent_ShouldAddEventAndInsulinEvent()
        {
            // Arrange
            var FakeDbContext = A.Fake<diabetiaContext>();

            var repository = new EventRepository(FakeDbContext);
            var email = "test@example.com";
            var idKindEvent = 1;
            var eventDate = DateTime.Now;
            var freeNote = "Test note";
            var insulin = 10;

            // Act
            await repository.AddInsulinEvent(email, idKindEvent, eventDate, freeNote, insulin);

            // Assert
            // Verificamos que el evento y el evento de insulina se hayan agregado correctamente
            var eventAdded = await FakeDbContext.CargaEventos.FirstOrDefaultAsync(e => e.FechaEvento == eventDate);
            var insulinEventAdded = await FakeDbContext.EventoInsulinas.FirstOrDefaultAsync(e => e.IdCargaEvento == eventAdded.Id);

            Assert.NotNull(eventAdded);
            Assert.NotNull(insulinEventAdded);

        }*/
    }
}
