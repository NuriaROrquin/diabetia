namespace Diabetia.Domain.Services
{
    public interface IAuthProvider
    {

        public Task<string> RegisterUserAsync(string username, string password, string email);

        public Task<bool> ConfirmEmailVerificationAsync(string username, string hashCode, string confirmationCode);

        public Task<string> LoginUserAsync(string username, string password);

        public Task ForgotPasswordRecoverAsync(string username);

        public Task ConfirmForgotPasswordCodeAsync(string username, string confirmationCode, string password);

        public Task ChangeUserPasswordAsync(string accessToken, string previousPassword, string newPassword);

        public string CalculateSecretHash(string clientId, string clientSecret, string username);


    }
}
