using Diabetia.Domain.Repositories;
using Diabetia.Domain.Utilities.Interfaces;

namespace Diabetia.Domain.Utilities.Validations
{
    public class HashValidator : IHashValidator
    {
        private readonly IAuthRepository _authRepository;
        public HashValidator(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<string> GetUserHash(string email)
        {
            var hashCode = await _authRepository.GetUserHashAsync(email);
            if (string.IsNullOrEmpty(hashCode))
            {
                throw new InvalidOperationException();
            }
            return hashCode;
        }
    }
}
