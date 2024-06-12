namespace Diabetia.API.DTO.EventRequest.MedicalVisit
{
    public class AddMedicalVisitRequest : BasicEventRequest

    {
        public int KindEventId { get; set; }

        public int ProfessionalId { get; set; }

        public bool Recordatory { get; set; }

        public DateTime RecordatoryDate { get; set; }

        public string Description { get; set; }
    }
}
