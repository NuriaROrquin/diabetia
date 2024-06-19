using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.MedicalVisit
{
    public class EditMedicalVisitRequest : BasicEventRequest
    {
        public int EventId { get; set; }
        public int ProfessionalId { get; set; }
        public bool Recordatory { get; set; }
        public DateTime? RecordatoryDate { get; set; }
        public string Description { get; set; }

        public EventoVisitaMedica ToDomain(EditMedicalVisitRequest request)
        {
            var medicalVisit = new EventoVisitaMedica();

            medicalVisit.IdCargaEventoNavigation.Id = request.EventId;
            medicalVisit.IdCargaEventoNavigation.FechaEvento = request.EventDate;
            medicalVisit.IdCargaEventoNavigation.NotaLibre = request.Description;
            medicalVisit.IdProfesional = request.ProfessionalId;
            medicalVisit.Descripcion = request.Description;

            return medicalVisit;
        }
    }
}
