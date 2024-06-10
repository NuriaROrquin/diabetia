using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO.EventRequest
{
    public class FoodManuallyRequest : EventRequest
    {
        public int? IdEvent { get; set; }
        public int IdKindEvent { get; set; }

        public int IdFoodChargeType { get; set; }

        public string? FreeNote { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
