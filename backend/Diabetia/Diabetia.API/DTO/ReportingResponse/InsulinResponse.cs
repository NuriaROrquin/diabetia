using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.ReportingResponse
{
    public class InsulinResponse
    {
        public int Value { get; set; }
        public string Time { get; set; }

        public static InsulinResponse FromInsulinEvent(EventoInsulina evento)
        {
            DateTime Date = evento.IdCargaEventoNavigation.FechaEvento;
            string stringDate = Date.ToString("dd/MM/yyyy");
            return new InsulinResponse
            {
                Value = evento.InsulinaInyectada.Value, 
                Time = stringDate
            };
        }
    }
}
