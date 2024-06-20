using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.Food
{
    public class AddFoodManuallyRequest : BasicEventRequest
    {
        public int KindEventId { get; set; }

        public string? FreeNote { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        public IEnumerable<string> UniqueId { get; set; }

        public EventoComidum ToDomain()
        {
            var manuallyFood = new EventoComidum();

            manuallyFood.IdCargaEventoNavigation = new CargaEvento
            {
                IdTipoEvento = KindEventId,
                FechaEvento = EventDate,
                NotaLibre = !string.IsNullOrEmpty(FreeNote) ? FreeNote : null,
            };

            return manuallyFood;
        }
    }
}
