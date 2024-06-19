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

        public EventoVisitaMedica ToDomain(AddMedicalVisitRequest request)
        {
            var medicalVisit = new EventoVisitaMedica();
            medicalVisit.IdCargaEventoNavigation.IdTipoEvento = request.KindEventId;
            medicalVisit.IdCargaEventoNavigation.FechaEvento = request.EventDate;
            medicalVisit.IdCargaEventoNavigation.NotaLibre = medicalVisit.IdCargaEventoNavigation.NotaLibre != null ? request.Description : null;

            //if (request.Recordatory)
            //{
            //    var recordatory = new Recordatorio();
            //    recordatory.FechaInicio = request.RecordatoryDate.Date;
            //    recordatory.HorarioActividad = request.RecordatoryDate.Hour;

            //    recordatoryEvent.IdCargaEvento = medicalVisit.IdCargaEvento;
            //    recordatoryEvent.FechaHoraRecordatorio = request.RecordatoryDate;
            //}
            
            medicalVisit.IdProfesional = request.ProfessionalId;
            medicalVisit.Descripcion = request.Description;
            return medicalVisit;
        }
    }
}
