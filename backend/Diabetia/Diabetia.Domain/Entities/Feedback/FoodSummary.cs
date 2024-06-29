namespace Diabetia.Domain.Entities.Feedback
{
    public class FoodSummary
    {
        public DateTime EventDate { get; set; }
        public int EventId { get; set; }
        public decimal Carbohydrates { get; set; }
        public int KindEventId { get; set;}
    }
}
