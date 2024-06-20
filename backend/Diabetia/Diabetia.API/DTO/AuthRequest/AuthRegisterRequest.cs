using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthRegisterRequest : AuthUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Usuario ToDomain(AuthRegisterRequest request)
        {
            var user = new Usuario();
            
            user.Email = request.Email;
            user.Username = request.Username;
            return user;
        }
    }
}