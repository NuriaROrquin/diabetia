namespace Diabetia.API.DTO.EventRequest
{
    public class FoodManuallyRequest : EventRequest
    {
        public int IdKindEvent { get; set; }

        public int IdFoodChargeType { get; set; }

        public string? FreeNote { get; set; }

        public decimal Quantity { get; set; }

        public int IdIngredient{ get; set; }




    }
}
