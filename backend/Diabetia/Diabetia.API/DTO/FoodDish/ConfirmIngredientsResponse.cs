namespace Diabetia.API.DTO.FoodDish
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
    }
}