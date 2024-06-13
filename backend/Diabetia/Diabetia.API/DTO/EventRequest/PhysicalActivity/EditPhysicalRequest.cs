using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.PhysicalActivity
{
    public class EditPhysicalRequest : BasicEventRequest
    {
        public int EventId { get; set; }
        public int PhysicalActivityId { get; set; }
        public TimeSpan IniciateTime { get; set; }
        public TimeSpan FinishTime { get; set; }
        public string FreeNote { get; set; }
    

        public EventoActividadFisica ToDomain(EditPhysicalRequest request)
        {
            var actFisica = new EventoActividadFisica();

            TimeSpan difference = request.FinishTime - request.IniciateTime;
            double totalMinutes = difference.TotalMinutes;
            actFisica.Duracion = (int)Math.Ceiling(totalMinutes);
            actFisica.IdActividadFisica = request.PhysicalActivityId;
            actFisica.IdCargaEventoNavigation.Id = EventId;
            actFisica.IdCargaEventoNavigation.FechaEvento = request.EventDate;
            actFisica.IdCargaEventoNavigation.NotaLibre = actFisica.IdCargaEventoNavigation.NotaLibre != null ? request.FreeNote : null;
            return actFisica;
        }
    }
}