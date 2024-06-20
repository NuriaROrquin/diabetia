using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthConfirmEmailRequest : AuthUserRequest
    {
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }

        public Usuario ToDomain(AuthConfirmEmailRequest request)
        {
            var user = new Usuario();

            user.Email = request.Email;
            user.Username = request.Username;
            return user;
        }
    }   
}
