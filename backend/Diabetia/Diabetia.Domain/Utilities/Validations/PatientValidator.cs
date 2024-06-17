using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Domain.Utilities.Validations
{
    public class PatientValidator : IPatientValidator
    {
        private readonly IUserRepository _userRepository;

        public PatientValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidatePatient(string email)
        {
            var patient = await _userRepository.GetPatient(email);
            if (patient == null)
            {
                throw new PatientNotFoundException();
            }
        }
    }
}
