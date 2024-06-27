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

        public EventoVisitaMedica ToDomain()
        {
            var medicalVisit = new EventoVisitaMedica()
            {
                IdProfesional = ProfessionalId,
                Descripcion = Description,
                IdCargaEventoNavigation = new CargaEvento()
                {
                    Id = EventId,
                    FechaEvento = EventDate,
                    NotaLibre = Description,
                }
            };
            return medicalVisit;
        }
    }
}
