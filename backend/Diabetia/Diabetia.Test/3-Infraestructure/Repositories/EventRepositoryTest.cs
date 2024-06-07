using Moq;
using Diabetia.Infrastructure.Repositories;
using Diabetia.Domain.Models;
using Moq.EntityFrameworkCore;
using Diabetia.Infrastructure.EF;
using Diabetia.API;
using Diabetia.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class EventRepositoryTests
{
    [Fact]
    public async Task EditPhysicalActivityEvent_ShouldUpdateEventAndPhysicalEvent()
    {
        // Arrange
        var mockContext = CreateMockContextPassCorrect();

        var repository = new EventRepository(mockContext.Object);

        var email = "test@example.com";
        var eventId = 1;
        var eventDate = DateTime.Now.AddDays(1);
        var physicalActivity = 1;
        var iniciateTime = new TimeSpan(10, 0, 0);
        var finishTime = new TimeSpan(11, 0, 0);
        var freeNote = "Updated Note";

        // Act
        await repository.EditPhysicalActivityEvent(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote);

        // Assert
        mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        var @event = mockContext.Object.CargaEventos.First();
        var physicalEvent = mockContext.Object.EventoActividadFisicas.First();

        Assert.Equal(eventDate, @event.FechaEvento);
        Assert.Equal(freeNote, @event.NotaLibre);
        Assert.Equal(60, physicalEvent.Duracion); // Assuming the duration is 60 minutes
    }

    private Mock<diabetiaContext> CreateMockContextPassCorrect()
    {
        var user = new Usuario { Id = 1, Email = "test@example.com" };
        var patient = new Paciente { Id = 1, IdUsuario = user.Id };
        var @event = new CargaEvento { Id = 1, IdPaciente = patient.Id, FechaEvento = DateTime.Now, NotaLibre = "Old Note" };
        var physicalEvent = new EventoActividadFisica { IdCargaEvento = 1, IdActividadFisica = 1, Duracion = 60 };

        var mockContext = new Mock<diabetiaContext>();

        mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
        mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });
        mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { @event });
        mockContext.Setup(m => m.EventoActividadFisicas).ReturnsDbSet(new List<EventoActividadFisica> { physicalEvent });

        return mockContext;
    }

    public async Task EditPhysicalActivityEvent_GivenInvalidEvent_ThrowsEventNotFoundException()
    {
        var mockContext = CreateMockContextThrowEventException();

        var repository = new EventRepository(mockContext.Object);

        var email = "test@example.com";
        var eventId = 1;


        //mockContext.Setup(m => m.CargaEventos.FirstOrDefaultAsync(ce => ce.Id == It.IsAny<int>()))
        //.ReturnsAsync((CargaEvento)null);

        //await Assert.ThrowsAsync<EventNotFoundException>(() => repository.EditPhysicalActivityEvent(email, eventId, eventDate, physicalActivity, iniciateTime, finishTime, freeNote));
    }

    private Mock<diabetiaContext> CreateMockContextThrowEventException()
    {
        var user = new Usuario { Id = 1, Email = "test@example.com" };
        var patient = new Paciente { Id = 1, IdUsuario = user.Id };

        var mockContext = new Mock<diabetiaContext>();

        mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
        mockContext.Setup(m => m.Pacientes).ReturnsDbSet(new List<Paciente> { patient });


        mockContext.Setup(m => m.CargaEventos).ReturnsAsync((DbSet<CargaEvento>)null);
        //CargaEvento nullEvent = null; // Variable 'event' asignada a null
        //mockContext.Setup(m => m.CargaEventos).ReturnsDbSet(new List<CargaEvento> { nullEvent });
        //mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });

        //mockContext.Setup(m => m.CargaEventos).Returns(Task.FromResult<DbSet<CargaEvento>>(null));
        //mockContext.Setup(m => m.CargaEventos).ReturnsAsync((DbSet<CargaEvento>)null);

        return mockContext;
    }
}
