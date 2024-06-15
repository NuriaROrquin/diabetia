using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Interfaces;

namespace Diabetia.Domain.Utilities.Validations
{
    public class PatientEventValidator : IPatientEventValidator
    {
        private readonly IEventRepository _eventRepository;

        public PatientEventValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task ValidatePatientEvent(string email, CargaEvento eventToValidate)
        {
            await _eventRepository.CheckPatientEvent(email, eventToValidate);
        }
    }
}
