using System.Security.Claims;

namespace Diabetia.Domain.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string userName, string email, bool initialFormCompleted);
    }
}
