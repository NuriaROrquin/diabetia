using System.ComponentModel.DataAnnotations;

namespace Diabetia.API.DTO.AuthRequest
{
    public class AuthChangePasswordRequest
    {

        public string AccessToken { get; set; }

        [Required(ErrorMessage = "La contraseña actual es requerida.")]
        public string PreviousPassword { get; set; }

        [Required(ErrorMessage = "La contraseña nueva es requerida.")]
        public string NewPassword { get; set; }
    }
}
