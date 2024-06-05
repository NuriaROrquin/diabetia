namespace Diabetia.Domain.Repositories
{
    public interface IAuthRepository
    {
        public Task SaveUserHashAsync(string username, string email, string hashCode);

        public Task<string> GetUserHashAsync(string email);

        public Task SaveUserUsernameAsync(string email, string username);

        public Task <string> GetUsernameByEmailAsync(string email);

        public Task SetUserStateActiveAsync(string email);

        public Task <bool> GetUserStateAsync(string email);

        public Task <bool> CheckUsernameOnDatabaseAsync(string username);

        public Task ResetUserAttemptsAsync(string username);
    }
}
