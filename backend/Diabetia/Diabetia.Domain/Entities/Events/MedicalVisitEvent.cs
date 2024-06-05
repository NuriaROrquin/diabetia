namespace Diabetia.Domain.Entities.Events
{
    public class MedicalVisitEvent
    {
        public int IdEvent { get; set; }
        public int IdEventType { get; set; }
        public DateTime DateEvent { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
