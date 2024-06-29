using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.Insuline
{
    public class AddInsulinRequest : BasicEventRequest
    {
        public int KindEventId { get; set; }
        public string? FreeNote { get; set; }
        public int InsulinInjected { get; set; }

        public EventoInsulina ToDomain()
        {
            var insulin = new EventoInsulina();

            insulin.IdCargaEventoNavigation = new CargaEvento
            {
                IdTipoEvento = KindEventId,
                FechaEvento = EventDate,
                NotaLibre = !string.IsNullOrEmpty(FreeNote) ? FreeNote : null,
            };
            insulin.InsulinaInyectada = InsulinInjected;
            return insulin;
        }
    }
}
