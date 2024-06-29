using Diabetia.Domain.Entities.Feedback;

namespace Diabetia.API.DTO.FeedbackResponse
{
    public class FoodEventResponse
    {
        public string EventDate { get; set; }

        public decimal Carbohydrates { get; set; }

        public static FoodEventResponse FromObject(FoodSummary foodSummary)
        {
            var response = new FoodEventResponse()
            {
                EventDate = foodSummary.EventDay.ToString("dd/MM/yyyy"),
                Carbohydrates = foodSummary.Carbohydrates
            };
            return response;
        }
    }
}
