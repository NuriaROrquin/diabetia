namespace Diabetia.API.DTO.EventRequest.PhysicalActivity
{
    public class AddPhysicalRequest : BasicEventRequest
    {
        public int IdKindEvent { get; set; }
        public DateTime EventDate { get; set; }
        public string? FreeNote { get; set; }
        public int PhysicalActivity { get; set; }
        public TimeSpan IniciateTime { get; set; }
        public TimeSpan FinishTime { get; set; }

    }
}
