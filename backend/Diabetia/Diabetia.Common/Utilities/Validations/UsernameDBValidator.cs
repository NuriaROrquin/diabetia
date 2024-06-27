using Diabetia.Application.Exceptions;
using Diabetia.Common.Utilities.Interfaces;
using Diabetia.Domain.Repositories;

namespace Diabetia.Common.Utilities.Validations
{
    public class UsernameDBValidator : IUsernameDBValidator
    {
        private readonly IAuthRepository _authRepository;
        public UsernameDBValidator(IAuthRepository authRepository) 
        {
            _authRepository = authRepository;
        }

        public async Task <string> CheckUsernameOnDB(string email)
        {
            string username = await _authRepository.GetUsernameByEmailAsync(email);
            if (username == "") 
            {
                throw new UsernameNotFoundException();
            }
            return username;
        }
    }
}
