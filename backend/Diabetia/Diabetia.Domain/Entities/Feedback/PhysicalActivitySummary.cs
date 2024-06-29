
namespace Diabetia.Domain.Entities.Feedback
{
    public class PhysicalActivitySummary
    {
        public DateTime EventDate { get; set; }
        public int EventId {  get; set; }
        public string ActivityName {  get; set; }

        public int KindEventId { get; set; }
    }
}
