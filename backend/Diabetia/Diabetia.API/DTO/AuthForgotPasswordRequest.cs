using System.ComponentModel.DataAnnotations;

namespace Diabetia.API.DTO
{
    public class AuthForgotPasswordRequest
    {
        [Required(ErrorMessage = "El email es requerido.")]
        public string Email { get; set; }
    }
}
