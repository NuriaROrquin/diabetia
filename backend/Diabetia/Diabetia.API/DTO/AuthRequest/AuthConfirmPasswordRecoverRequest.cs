using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthConfirmPasswordRecoverRequest
    {
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }
        public string Password { get; set; }

        public Usuario ToDomain(AuthConfirmPasswordRecoverRequest request)
        {
            var user = new Usuario();

            user.Email = request.Email;
            return user;
        }
    }
}
