using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.PhysicalActivity
{
    public class AddPhysicalRequest : BasicEventRequest
    {
        public int KindEventId { get; set; }
        public string? FreeNote { get; set; }
        public int PhysicalActivityId { get; set; }
        public TimeSpan IniciateTime { get; set; }
        public TimeSpan FinishTime { get; set; }

        public EventoActividadFisica ToDomain()
        {
            var actFisica = new EventoActividadFisica();

            TimeSpan difference = FinishTime - IniciateTime;
            double totalMinutes = difference.TotalMinutes;
            actFisica.Duracion = (int)Math.Ceiling(totalMinutes);
            actFisica.IdActividadFisica = PhysicalActivityId;

            actFisica.IdCargaEventoNavigation = new CargaEvento
            {
                IdTipoEvento = KindEventId,
                FechaEvento = EventDate,
                NotaLibre = !string.IsNullOrEmpty(FreeNote) ? FreeNote : null,
            };

            return actFisica;
        }
    }
}