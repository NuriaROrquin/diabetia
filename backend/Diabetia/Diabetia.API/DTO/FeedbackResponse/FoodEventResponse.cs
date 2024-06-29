using Diabetia.Domain.Entities.Feedback;

namespace Diabetia.API.DTO.FeedbackResponse
{
    public class FoodEventResponse
    {
        public string EventDate { get; set; }

        public int EventId { get; set; }

        public decimal Carbohydrates { get; set; }
        
        public int KindEventId { get; set; }
    }
}
