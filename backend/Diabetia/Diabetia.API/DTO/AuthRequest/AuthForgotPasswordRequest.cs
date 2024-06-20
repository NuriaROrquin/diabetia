using Diabetia.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthForgotPasswordRequest
    {
        [Required(ErrorMessage = "El email es requerido.")]
        public string Email { get; set; }

        public Usuario ToDomain(AuthForgotPasswordRequest request)
        {
            var user = new Usuario();

            user.Email = request.Email;
            return user;
        }
    }
}
