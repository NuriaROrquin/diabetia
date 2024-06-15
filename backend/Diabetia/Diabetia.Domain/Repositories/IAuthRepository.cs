using Diabetia.Domain.Models;

namespace Diabetia.Domain.Repositories
{
    public interface IAuthRepository
    {
        public Task SaveUserHashAsync(Usuario user, string hashCode);

        public Task<string> GetUserHashAsync(string email);

        public Task SaveUserUsernameAsync(Usuario user);

        public Task <string> GetUsernameByEmailAsync(string email);

        public Task SetUserStateActiveAsync(string email);

        public Task <bool> GetUserStateAsync(string email);

        public Task <bool> CheckUsernameOnDatabaseAsync(string username);

        public Task CheckEmailOnDatabaseAsync(string email);

        public Task ResetUserAttemptsAsync(string username);
    }
}
