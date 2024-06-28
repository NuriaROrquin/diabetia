namespace Diabetia.Domain.Entities.Events
{
    public class PhysicalActivityEvent
    {
        public int IdEvent { get; set; }
        public int IdEventType { get; set; }
        public int IdPhysicalEducationEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public string TypeActivity { get; set; }
    }
}
