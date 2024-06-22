using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities.Events;
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

namespace Diabetia_Core.Events
{
    public class MedicalVisitUseCaseTest
    {
        private readonly IEventRepository _fakeEventRepository;
        private readonly IPatientValidator _fakePatientValidator;
        private readonly IPatientEventValidator _fakePatientEventValidator;
        private readonly IUserRepository _fakeUserRepository;
        private readonly MedicalVisitUseCase _fakeMedicalVisitUseCase;
        public MedicalVisitUseCaseTest()
        {
            _fakeEventRepository = A.Fake<IEventRepository>();
            _fakePatientValidator = A.Fake<IPatientValidator>();
            _fakePatientEventValidator = A.Fake<IPatientEventValidator>();
            _fakeUserRepository = A.Fake<IUserRepository>();
            _fakeMedicalVisitUseCase = new MedicalVisitUseCase(_fakeEventRepository, _fakePatientValidator, _fakePatientEventValidator, _fakeUserRepository);
        }

        // --------------------------------------- ⬇⬇ Add Medical Event ⬇⬇ ---------------------------------------
        [Fact]
        public async Task EventMedicalVisitUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            var email = "emailTest@example.com";
            var patient = new Paciente()
            {
                Id = 1
            };
            var medicalEvent = new EventoVisitaMedica()
            {
                IdProfesional = 1,
                Descripcion = "Testing",
                IdCargaEventoNavigation = new CargaEvento()
                {
                    Id = 1,
                }
            };

            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patient);
       
            // Act
            await _fakeMedicalVisitUseCase.AddMedicalVisitEventAsync(email, medicalEvent);

            // Assert 
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.AddMedicalVisitEventAsync(patient.Id, medicalEvent)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EventMedicalVisitUseCase_WhenCalledInvalidPatient_ThrowsPatientNotFoundException()
        {
            var email = "emailTest@example.com";
            var patient = new Paciente()
            {
                Id = 1
            };
            var medicalEvent = new EventoVisitaMedica()
            {
                IdProfesional = 1,
                Descripcion = "Testing",
                IdCargaEventoNavigation = new CargaEvento()
                {
                    Id = 1,
                }
            };

            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Throws<PatientNotFoundException>();

            // Act & Assert 
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakeMedicalVisitUseCase.AddMedicalVisitEventAsync(email, medicalEvent));
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }


        // --------------------------------------- ⬇⬇ Edit Medical Event ⬇⬇ ---------------------------------------
        [Fact]
        public async Task EditMedicalVisitUseCase_WhenCalledWithValidData_ShouldUpdateEventSuccessfully()
        {
            // Assert
            var email = "emailTest@example.com";
            var medicalVisit = new EventoVisitaMedica()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 1,
                }
            };

            var @event = new CargaEvento()
            {
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = false
            };

            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(medicalVisit.IdCargaEventoNavigation.Id)).Returns(@event);

            // Act
            await _fakeMedicalVisitUseCase.EditMedicalVisitEventAsync(email, medicalVisit);

            // Assert 
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(medicalVisit.IdCargaEventoNavigation.Id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.EditMedicalVisitEventAsync(medicalVisit)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditMedicalVisitUseCase_WhenCalledWithInvalidPatient_ThrowsPatientNotFoundException()
        {
            // Assert
            var email = "emailTest@example.com";
            var medicalVisit = new EventoVisitaMedica()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 1,
                }
            };

            var @event = new CargaEvento()
            {
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = false
            };
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            // Act & Assert 
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakeMedicalVisitUseCase.EditMedicalVisitEventAsync(email, medicalVisit));

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task EditMedicalVisitUseCase_WhenCalledWithValidPatientInvalidEvent_ThrowsEventNotRelatedWithPatientException()
        {
            // Assert
            var email = "emailTest@example.com";
            var medicalVisit = new EventoVisitaMedica()
            {
                IdCargaEventoNavigation = new CargaEvento
                {
                    IdTipoEvento = 1,
                }
            };

            var @event = new CargaEvento()
            {
                FechaEvento = DateTime.Now.AddDays(1),
                NotaLibre = "Test Note",
                FechaActual = DateTime.Now,
                FueRealizado = false,
                EsNotaLibre = false
            };
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(medicalVisit.IdCargaEventoNavigation.Id)).Returns(@event);
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).Throws<EventNotRelatedWithPatientException>();

            // Act & Assert 
            await Assert.ThrowsAsync<EventNotRelatedWithPatientException>(() => _fakeMedicalVisitUseCase.EditMedicalVisitEventAsync(email, medicalVisit));

            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeEventRepository.GetEventByIdAsync(medicalVisit.IdCargaEventoNavigation.Id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakePatientEventValidator.ValidatePatientEvent(email, @event)).MustHaveHappenedOnceExactly();
        }
    }
}
