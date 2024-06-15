using Diabetia.Domain.Utilities.Interfaces;
using Diabetia.Domain.Repositories;

namespace Diabetia.Domain.Utilities.Validations
{
    public class EmailDBValidator : IEmailDBValidator
    {
        private readonly IAuthRepository _authRepository;

        public EmailDBValidator(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task CheckEmailOnDB(string email)
        {
            await _authRepository.CheckEmailOnDatabaseAsync(email);
        }
    }
}
