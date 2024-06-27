using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.Insuline
{
    public class EditInsulinRequest : BasicEventRequest
    {
        public int EventId { get; set; }
        public string? FreeNote { get; set; }
        public int InsulinInjected { get; set; }

        public EventoInsulina ToDomain()
        {
            var insulin = new EventoInsulina();

            insulin.IdCargaEventoNavigation = new CargaEvento
            {
                Id = EventId,
                FechaEvento = EventDate,
                NotaLibre = !string.IsNullOrEmpty(FreeNote) ? FreeNote : null,
            };
            insulin.InsulinaInyectada = InsulinInjected;
            return insulin;
        }
    }
}