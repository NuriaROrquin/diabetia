using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.PhysicalActivity
{
    public class EditPhysicalRequest : BasicEventRequest
    {
        public int EventId { get; set; }
        public int PhysicalActivityId { get; set; }
        public TimeSpan IniciateTime { get; set; }
        public TimeSpan FinishTime { get; set; }
        public string? FreeNote { get; set; }
    

        public EventoActividadFisica ToDomain()
        {
            var actFisica = new EventoActividadFisica();

            TimeSpan difference = FinishTime - IniciateTime;
            double totalMinutes = difference.TotalMinutes;
            actFisica.Duracion = (int)Math.Ceiling(totalMinutes);
            actFisica.IdActividadFisica = PhysicalActivityId;

            actFisica.IdCargaEventoNavigation = new CargaEvento
            {
                Id = EventId,
                NotaLibre = !string.IsNullOrEmpty(FreeNote) ? FreeNote : null,
                FechaEvento = EventDate
            };
            return actFisica;
        }
    }
}