using Diabetia.Domain.Services;

namespace Diabetia.Common.Utilities.Validations
{
    public class PatientValidator
    {
        private readonly IUserRepository _userRepository;

        public PatientValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidatePatient(string email)
        {
            _ = _userRepository.GetPatient(email);
        }
    }
}
