namespace Diabetia.API.DTO.FoodDishRequest
{
    public class ConfirmQuantityRequest
    {
        public List<Ingredient> Ingredients { get; set; }
    }

    public class Ingredient
    {
        public string Carbohydrates { get; set; }
        public int Quantity { get; set; }
    }
}
