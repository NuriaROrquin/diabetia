namespace Diabetia.API.DTO
{
    public class EventRequest
    {
        public string Email { get; set; }
        public int IdKindEvent { get; set; }
        public DateTime EventDate { get; set; }
        public string? FreeNote { get; set; }
        public int PhysicalActivity { get; set; }
        public TimeSpan IniciateTime { get; set; }
        public TimeSpan FinishTime { get; set; }


    }
}
