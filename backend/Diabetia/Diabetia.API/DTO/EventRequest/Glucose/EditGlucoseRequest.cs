using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.EventRequest.Glucose
{
    public class EditGlucoseRequest : BasicEventRequest
    {
        public int EventId { get; set; }
        public string? FreeNote { get; set; }
        public decimal Glucose { get; set; }
        public int? PatientDeviceId { get; set; }
        public int? IdFoodEvent { get; set; }
        public bool? PostFoodMedition { get; set; }

        public EventoGlucosa ToDomain()
        {
            var glucose = new EventoGlucosa();

            glucose.IdCargaEventoNavigation = new CargaEvento
            {
                Id = EventId,
                FechaEvento = EventDate,
                NotaLibre = !string.IsNullOrEmpty(FreeNote) ? FreeNote : null,
            };
            glucose.IdDispositivoPaciente = PatientDeviceId;
            glucose.Glucemia = Glucose;
            glucose.IdEventoComida = IdFoodEvent;
            glucose.MedicionPostComida = PostFoodMedition;
            return glucose;
        }
    }
}
