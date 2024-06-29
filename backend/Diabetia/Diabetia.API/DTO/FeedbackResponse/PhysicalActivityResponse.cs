using Diabetia.Domain.Entities.Feedback;

namespace Diabetia.API.DTO.FeedbackResponse
{
    public class PhysicalActivityResponse
    {
        public string EventDate { get; set; }

        public string Name { get; set; }

        public static PhysicalActivityResponse FromObject(PhysicalActivitySummary activitySummary) 
        {
            var response = new PhysicalActivityResponse()
            {
                EventDate = activitySummary.EventDay.ToString("dd/MM/yyyy"),
                Name = activitySummary.ActivityName
            };

            return response;
        }
    }
}
