using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO.FoodDishRequest
{
    public class ConfirmQuantityRequest
    {
        public List<IngredientQuantity> Ingredients { get; set; }

        public ConfirmQuantityRequest()
        {
            Ingredients = new List<IngredientQuantity>();
        }

        public List<FoodInfo> ToDomain()
        {
            return Ingredients.Select(tag => new FoodInfo
            {
                Carbohydrates = tag.Carbohydrates,
                Quantity = tag.Quantity
            }).ToList();
        }
    }

    public class IngredientQuantity
    {
        public double Carbohydrates { get; set; }
        public int Quantity { get; set; }
    }
}
