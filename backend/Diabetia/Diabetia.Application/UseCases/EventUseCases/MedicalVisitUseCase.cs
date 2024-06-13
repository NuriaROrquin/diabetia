using Diabetia.Application.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.EventUseCases
{
    public class MedicalVisitUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;
        private readonly IPatientEventValidator _patientEventValidator;
        public MedicalVisitUseCase(IEventRepository eventRepository, IPatientValidator patientValidator, IPatientEventValidator patientEventValidator)
        {
            _eventRepository = eventRepository;
            _patientValidator = patientValidator;
            _patientEventValidator = patientEventValidator;
        }

        public async Task AddMedicalVisitEventAsync(string email, EventoVisitaMedica medicalVisit)
        {
            await _patientValidator.ValidatePatient(email);
            await _eventRepository.AddMedicalVisitEventAsync(medicalVisit);
            //if (recordatoryEvent != null)
            //{
            //    await _recordatoryRepository.AddRecordatoryEventAsync(medicaLVisit.IdCargaEventoNavigation.Id, recordatoryEvent);
            //}
        }

        public async Task EditMedicalVisitEventAsync(string email, EventoVisitaMedica medicalVisit)
        {
            await _patientValidator.ValidatePatient(email);
            var @event = await _eventRepository.GetEventByIdAsync(medicalVisit.IdCargaEventoNavigation.Id);
            if (@event == null)
            {
                throw new EventNotFoundException();
            }
            await _patientEventValidator.ValidatePatientEvent(email, @event);
            await _eventRepository.EditMedicalVisitEventAsync(medicalVisit);
        }
    }
}