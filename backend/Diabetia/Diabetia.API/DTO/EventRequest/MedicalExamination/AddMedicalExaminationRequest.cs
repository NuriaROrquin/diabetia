using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.MedicalExamination
{
    public class AddMedicalExaminationRequest : BasicEventRequest
    {
        public string File { get; set; }
        public string ExaminationType { get; set; }
        public int? IdProfessional { get; set; }
        public int KindEventId { get; set; }

        public EventoEstudio ToDomain()
        {
            var medicalExamination = new EventoEstudio();

            medicalExamination.Archivo = File;
            medicalExamination.IdProfesional = IdProfessional;
            medicalExamination.IdCargaEventoNavigation = new CargaEvento()
            {
                IdTipoEvento = KindEventId,
                FechaEvento = EventDate,
                NotaLibre = ExaminationType
            };
            return medicalExamination;
        }
    }
}
