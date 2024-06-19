using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO
{
    public class IngredientResponse
    {
        public IEnumerable<AdditionalDataIngredient> Ingredients { get; set; }
    }
}
