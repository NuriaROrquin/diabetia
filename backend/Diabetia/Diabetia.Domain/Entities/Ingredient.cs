namespace Diabetia.Domain.Entities
{
    public class Ingredient
    {
        public decimal Quantity { get; set; }
        public int IdIngredient { get; set; }
    }

    public class AdditionalDataIngredient : Ingredient {
    
        public string Name { get; set; }
        public Unit Unit { get; set; }
    }

    public class Unit
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
    }

}
