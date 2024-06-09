using System.ComponentModel.DataAnnotations;

namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthForgotPasswordRequest
    {
        [Required(ErrorMessage = "El email es requerido.")]
        public string Email { get; set; }
    }
}
