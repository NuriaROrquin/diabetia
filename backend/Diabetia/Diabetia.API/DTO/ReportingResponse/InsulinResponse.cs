using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.ReportingResponse
{
    public class InsulinResponse
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public static InsulinResponse FromInsulinEvent(EventoInsulina evento)
        {
            return new InsulinResponse
            {
                Value = evento.InsulinaInyectada.Value, 
                Name = ""
            };
        }
    }
}
