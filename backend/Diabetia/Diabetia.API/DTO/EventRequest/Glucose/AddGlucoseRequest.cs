using Diabetia.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Diabetia.API.DTO.EventRequest.Glucose
{
    public class AddGlucoseRequest : BasicEventRequest
    {
        public int KindEventId { get; set; }
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
                IdTipoEvento = KindEventId,
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
