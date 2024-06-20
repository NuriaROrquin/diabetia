namespace Diabetia.Domain.Entities.Events
{
    public class GlucoseEvent
    {
        public int IdEvent { get; set; }
        public int IdEventType { get; set; }
        public DateTime DateEvent { get; set; }
        public string Title { get; set; }
        public decimal GlucoseLevel { get; set; }
        public string? FreeNote { get; set; }
        public int? IdDevice { get; set; }
    }
}
