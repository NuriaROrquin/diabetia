using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.MedicalVisit
{
    public class AddMedicalVisitRequest : BasicEventRequest

    {
        public int KindEventId { get; set; }
        public int ProfessionalId { get; set; }
        public bool Recordatory { get; set; }
        public DateTime? RecordatoryDate { get; set; }
        public string Description { get; set; }

        public EventoVisitaMedica ToDomain()
        {
            var medicalVisit = new EventoVisitaMedica()
            {
                IdProfesional = ProfessionalId,
                Descripcion = Description != null ? Description : null,
                IdCargaEventoNavigation = new CargaEvento()
                {
                    IdTipoEvento = KindEventId,
                    FechaEvento = EventDate,
                    NotaLibre = Description != null ? Description : null,
                    EsNotaLibre = false
                }
            };
            return medicalVisit;
        }
    }
}
