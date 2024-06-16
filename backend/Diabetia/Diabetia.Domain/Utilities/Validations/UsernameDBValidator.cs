using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Utilities.Interfaces;
using Diabetia.Domain.Repositories;

namespace Diabetia.Domain.Utilities.Validations
{
    public class UsernameDBValidator : IUsernameDBValidator
    {
        private readonly IAuthRepository _authRepository;
        public UsernameDBValidator(IAuthRepository authRepository) 
        {
            _authRepository = authRepository;
        }

        public async Task <string> GetUsernameByEmail(string email)
        {
            string username = await _authRepository.GetUsernameByEmailAsync(email);
            if (username == "") 
            {
                throw new UsernameNotFoundException();
            }
            return username;
        }

        public async Task CheckUsernameOnDataBase(string username)
        {
            var userExists = await _authRepository.CheckUsernameOnDatabaseAsync(username);
            if (!userExists) 
            {
                throw new UsernameNotFoundException();
            }
        }
    }
}
