namespace Diabetia.API.DTO.EventRequest.MedicalExamination
{
    public class EventMedicalExaminationRequest : BasicEventRequest
    {
        public string File { get; set; }
        public string ExaminationType { get; set; }
        public int? IdProfessional { get; set; }
        public string? FreeNote { get; set; }

    }
}
