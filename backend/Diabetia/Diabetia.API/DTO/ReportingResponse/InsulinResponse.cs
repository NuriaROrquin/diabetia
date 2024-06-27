using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.ReportingResponse
{
    public class InsulinResponse
    {
        public int Value { get; set; }
        public string Time { get; set; }

        public static InsulinResponse FromInsulinEvent(EventoInsulina evento)
        {
            DateTime date = evento.IdCargaEventoNavigation.FechaEvento;
            string stringDate = date.ToString("dd/MM/yyyy");
            return new InsulinResponse
            {
                Value = evento.InsulinaInyectada.Value, 
                Time = stringDate
            };
        }
    }
}
