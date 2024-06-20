namespace Diabetia.Domain.Entities.Events
{
    public class InsulinEvent
    {
        public int IdEvent { get; set; }
        public int IdEventType { get; set; }
        public DateTime DateEvent { get; set; }
        public string Title { get; set; }
        public string InsulinType { get; set; }
        public int? Dosage { get; set; }
        public string? FreeNote { get; set; }
    }
}
