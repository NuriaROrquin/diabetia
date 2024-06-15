using Diabetia.Application.Exceptions;
using Diabetia.Common.Utilities.Interfaces;
using Diabetia.Domain.Repositories;

namespace Diabetia.Common.Utilities.Validations
{
    public class UserStatusValidator : IUserStatusValidator
    {
        private readonly IAuthRepository _authRepository;
        public UserStatusValidator(IAuthRepository authRepository) 
        {
            _authRepository = authRepository;
        }
        public async Task checkUserStatus(string email)
        {
            var userState = await _authRepository.GetUserStateAsync(email);
            if (!userState)
            {
                throw new UserNotAuthorizedException();
            }
        }
    }
}
