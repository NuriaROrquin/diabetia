using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO.EventRequest
{
    public class EventFoodRequest : BasicEventRequest
    {
        public int? IdEvent { get; set; }
        public int? IdKindEvent { get; set; }

        public string? FreeNote { get; set; }

        public IEnumerable<Ingredient>? Ingredients { get; set; }

        public IEnumerable<string>? UniqueId { get; set; }
    }
}
