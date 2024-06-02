namespace Diabetia.Domain.Repositories
{
    public interface IAuthRepository
    {
        public Task SaveUserHashAsync(string username, string email, string hashCode);

        public Task<string> GetUserHashAsync(string email);

        public Task SaveUserUsernameAsync(string email, string username);

        public Task <string> GetUsernameByEmail(string email);

        public Task SetUserActiveAsync(string email);
    }
}
