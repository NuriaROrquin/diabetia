using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.PhysicalActivity
{
    public class AddPhysicalRequest : BasicEventRequest
    {
        public int KindEventId { get; set; }
        public DateTime EventDate { get; set; }
        public string? FreeNote { get; set; }
        public int PhysicalActivityId { get; set; }
        public TimeSpan IniciateTime { get; set; }
        public TimeSpan FinishTime { get; set; }


        public EventoActividadFisica ToDomain(AddPhysicalRequest request)
        {
            var actFisica = new EventoActividadFisica();

            TimeSpan difference = request.FinishTime - request.IniciateTime;
            double totalMinutes = difference.TotalMinutes;
            actFisica.Duracion = (int)Math.Ceiling(totalMinutes);
            actFisica.IdActividadFisica = request.PhysicalActivityId;
            actFisica.IdCargaEventoNavigation.IdTipoEvento = request.KindEventId;
            actFisica.IdCargaEventoNavigation.FechaEvento = request.EventDate;
            actFisica.IdCargaEventoNavigation.NotaLibre = actFisica.IdCargaEventoNavigation.NotaLibre != null ? request.FreeNote : null;
            return actFisica;
        }
    }
}
