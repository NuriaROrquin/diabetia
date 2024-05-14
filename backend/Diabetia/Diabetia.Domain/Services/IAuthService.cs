namespace Diabetia.Domain.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email);
    }
}
