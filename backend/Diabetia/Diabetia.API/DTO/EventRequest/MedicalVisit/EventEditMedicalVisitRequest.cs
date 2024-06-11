namespace Diabetia.API.DTO.EventRequest.MedicalVisit
{
    public class EventEditMedicalVisitRequest : EventRequest
    {
        public int EventId { get; set; }
        public int ProfessionalId { get; set; }
        public bool Recordatory { get; set; }
        public DateTime? RecordatoryDate { get; set; }
        public string Description {  get; set; }
    }
}
