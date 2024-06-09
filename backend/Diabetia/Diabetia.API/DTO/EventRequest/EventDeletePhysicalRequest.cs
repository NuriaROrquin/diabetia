using System.ComponentModel.DataAnnotations;

namespace Diabetia.API.DTO.EventRequest
{
    public class EventDeletePhysicalRequest
    {
        [Required(ErrorMessage = "El Email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Evento es requerido")]
        public int EventId { get; set; }

    }
}
