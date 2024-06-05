namespace Diabetia.Domain.Entities.Events
{
    public class FoodEvent
    {
        public int IdEvent { get; set; }
        public int IdEventType { get; set; }
        public DateTime DateEvent { get; set; }
        public string Title { get; set; }
        public string IngredientName { get; set; }
    }
}
