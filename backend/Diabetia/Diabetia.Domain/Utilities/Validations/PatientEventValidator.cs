using Diabetia.Domain.Exceptions;
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

        /// <summary>
        /// Este método valida que el paciente tenga realmente asociado el evento.
        /// </summary>
        public async Task ValidatePatientEvent(string email, CargaEvento eventToValidate)
        {
            var check = await _eventRepository.CheckPatientEvent(email, eventToValidate);
            if (!check) 
            {
                throw new EventNotRelatedWithPatientException();
            }
        }
    }
}
