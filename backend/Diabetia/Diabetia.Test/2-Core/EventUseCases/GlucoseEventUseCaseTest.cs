using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Test._2_Core.EventUseCases
{
    public class GlucoseEventUseCaseTest
    {
        [Fact]
        public async Task AddGlucoseEventUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var glucoseEvent = new EventoGlucosa();
            var patient = new Paciente()
            {
                Id = 11
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeGlucoseEventUseCase = new GlucoseUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakeUserRepository.GetPatient(email)).Returns(patient);
            await fakeGlucoseEventUseCase.AddGlucoseEventAsync(email, glucoseEvent);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.AddGlucoseEventAsync(patient.Id, glucoseEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddGlucoseEventUseCase_WhenCalledInvalidPatient_ThrowsPatientNotFoundException()
        {
            var email = "emailTest@example.com";
            var glucoseEvent = new EventoGlucosa();
            var patient = new Paciente()
            {
                Id = 11
            };

            var fakeEventRepository = A.Fake<IEventRepository>();
            var fakePatientValidator = A.Fake<IPatientValidator>();
            var fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            var fakeUserRepository = A.Fake<IUserRepository>();

            var fakeGlucoseEventUseCase = new GlucoseUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();


            await Assert.ThrowsAsync<PatientNotFoundException>(() => fakeGlucoseEventUseCase.AddGlucoseEventAsync(email, glucoseEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task EditGlucoseEventUseCase_WhenCalledWithValidData_ShouldUpdateEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var glucoseEvent = new EventoGlucosa()
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

            A.CallTo(() => fakeEventRepository.GetEventByIdAsync(glucoseEvent.IdCargaEventoNavigation.Id)).Returns(loadedEvent);

            var fakeGlucoseEventUseCase = new GlucoseUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            await fakeGlucoseEventUseCase.EditGlucoseEventAsync(email, glucoseEvent);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakePatientEventValidator.ValidatePatientEvent(email, loadedEvent)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.EditGlucoseEventAsync(glucoseEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditGlucoseEventUseCase_WhenCalledInvalidPatient_ThrowsPatientNotFoundException()
        {
            var email = "emailTest@example.com";
            var glucoseEvent = new EventoGlucosa()
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

            var fakeEventGlucoseUseCase = new GlucoseUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            await Assert.ThrowsAsync<PatientNotFoundException>(() => fakeEventGlucoseUseCase.EditGlucoseEventAsync(email, glucoseEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task EditGlucoseUseCase_WhenCalledValidPatientInvalidEvent_ThrowsEventNotRelatedWithPatientException()
        {
            var email = "emailTest@example.com";
            var GlucoseEvent = new EventoGlucosa()
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

            var fakeEventGlucoseUseCase = new GlucoseUseCase(fakeEventRepository, fakePatientValidator, fakePatientEventValidator, fakeUserRepository);

            A.CallTo(() => fakeEventRepository.GetEventByIdAsync(GlucoseEvent.IdCargaEventoNavigation.Id)).Returns(loadedEvent);
            A.CallTo(() => fakePatientEventValidator.ValidatePatientEvent(email, loadedEvent)).Throws<EventNotRelatedWithPatientException>();

            // Act & Assert 
            await Assert.ThrowsAsync<EventNotRelatedWithPatientException>(() => fakeEventGlucoseUseCase.EditGlucoseEventAsync(email, GlucoseEvent));

            A.CallTo(() => fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEventRepository.GetEventByIdAsync(GlucoseEvent.IdCargaEventoNavigation.Id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakePatientEventValidator.ValidatePatientEvent(email, loadedEvent)).MustHaveHappenedOnceExactly();
        }
    }
}
