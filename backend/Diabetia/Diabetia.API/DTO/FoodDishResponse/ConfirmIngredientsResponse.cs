using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO.FoodDishResponse
{
    public class ConfirmIngredientsResponse
    {
        public ConfirmIngredientsResponse(IngredientsDetected ingredientsDetected)
        {
            Ingredients = ingredientsDetected.Ingredients.Select(i => new IngredientConfirmed
            {
                FoodItemPosition = i.FoodItemPosition,
                CarbohydratesPerPortion = i.CarbohydratesPerPortion,
                GrPerPortion = i.GrPerPortion,
                Name = i.Name
            }).ToList();
        }
        public List<IngredientConfirmed> Ingredients { get; set; }
    }

    public class IngredientConfirmed
    {
        public int FoodItemPosition { get; set; }
        public double CarbohydratesPerPortion { get; set; }
        public double GrPerPortion { get; set; }
        public string[] Name { get; set; }
    }
}