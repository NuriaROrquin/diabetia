using Diabetia.Domain.Entities.Feedback;

namespace Diabetia.API.DTO.FeedbackResponse
{
    public class PhysicalActivityResponse
    {
        public string EventDate { get; set; }

        public int EventId { get; set; }

        public string ActivityName { get; set; }
        
        public int KindEventId { get; set; }
    }
}
