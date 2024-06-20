using Xunit;
using FakeItEasy;
using System.Threading.Tasks;
using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using Diabetia.Interfaces;
using Diabetia.Domain.Exceptions;

namespace Diabetia.Test._2_Core.InsulinEventUseCases
{
    public class InsulinUseCaseTests
    {
        [Fact]
        public async Task AddInsulinEventUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var insulinEvent = new EventoInsulina();
            var patient = new Paciente()
            {
                Id = 11
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeInsulinEventUseCase = new InsulinUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakeUserRepository.GetPatient(email)).Returns(patient);
            await fakeInsulinEventUseCase.AddInsulinEventAsync(email, insulinEvent);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.AddInsulinEventAsync(patient.Id, insulinEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddInsulinEventUseCase_WhenCalledInvalidPatient_ThrowsPatientNotFoundException()
        {
            var email = "emailTest@example.com";
            var insulinEvent = new EventoInsulina();
            var patient = new Paciente()
            {
                Id = 11
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeInsulinEventUseCase = new InsulinUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();


            await Assert.ThrowsAsync<PatientNotFoundException>(() => fakeInsulinEventUseCase.AddInsulinEventAsync(email, insulinEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditInsulinEventUseCase_WhenCalledWithValidData_ShouldUpdateEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var insulinEvent = new EventoInsulina()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 1,
                }
            };

            var loadedEvent = new CargaEvento()
            {
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = false
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            A.CallTo(() => fakeEventRepository.GetEventByIdAsync(insulinEvent.IdCargaEventoNavigation.Id)).Returns(loadedEvent);

            var fakeInsulinEventUseCase = new InsulinUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            await fakeInsulinEventUseCase.EditInsulinEventAsync(email, insulinEvent);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakePatientEventValidator.ValidatePatientEvent(email, loadedEvent)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.EditInsulinEventAsync(insulinEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditInsulinEventUseCase_WhenCalledInvalidPatient_ThrowsPatientNotFoundException()
        {
            var email = "emailTest@example.com";
            var insulinEvent = new EventoInsulina()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 11,
                }
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeEventInsulinUseCase = new InsulinUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            await Assert.ThrowsAsync<PatientNotFoundException>(() => fakeEventInsulinUseCase.EditInsulinEventAsync(email, insulinEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditInsulinEventUseCase_WhenCalledValidPatientInvalidEvent_ThrowsEventNotRelatedWithPatientException()
        {
            var email = "emailTest@example.com";
            var insulinEvent = new EventoInsulina()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 11,
                }
            };

            var loadedEvent = new CargaEvento()
            {
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = false
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeInsulinUseCase = new InsulinUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakeEventRepository.GetEventByIdAsync(insulinEvent.IdCargaEventoNavigation.Id)).Returns(loadedEvent);
            A.CallTo(() => fakePatientEventValidator.ValidatePatientEvent(email, loadedEvent)).Throws<EventNotRelatedWithPatientException>();

            await Assert.ThrowsAsync<EventNotRelatedWithPatientException>(() => fakeInsulinUseCase.EditInsulinEventAsync(email, insulinEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.GetEventByIdAsync(insulinEvent.IdCargaEventoNavigation.Id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakePatientEventValidator.ValidatePatientEvent(email, loadedEvent)).MustHaveHappenedOnceExactly();
        }

    }
}
