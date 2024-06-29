using Diabetia.Domain.Entities;

namespace Diabetia.API.DTO.FoodDishResponse
{
    public class ConfirmIngredientsResponse
    {
        public List<IngredientConfirmed> Ingredients { get; set; }
    }

    public class IngredientConfirmed
    {
        public int FoodItemPosition { get; set; }
        public double CarbohydratesPerPortion { get; set; }
        public double GrPerPortion { get; set; }

        public IngredientsDetected ToDomain()
        {
            var ingredientsDetected = new IngredientsDetected()
            {
                Ingredients = this.Ingredients.Select(i => new IngredientsRecognized
                {
                    CarbohydratesPerPortion = i.CarbohydratesPerPortion,
                    GrPerPortion = i.GrPerPortion,
                    FoodItemPosition = i.FoodItemPosition
                }).ToList()
            };

            return ingredientsDetected;
        }
    }



}