namespace Diabetia.API.DTO.FoodDishRequest
{
    public class ConfirmQuantityRequest
    {
        public List<IngredientQuantity> Ingredients { get; set; }
    }

    public class IngredientQuantity
    {
        public string Carbohydrates { get; set; }
        public int Quantity { get; set; }
    }
}
