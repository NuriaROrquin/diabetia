using System.ComponentModel.DataAnnotations;

namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthUserRequest
    {
        [Required(ErrorMessage = "El usuario es requerido.")]
        public string Username { get; set; }
    }
}
