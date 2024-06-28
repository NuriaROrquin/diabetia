using Diabetia.Domain.Entities.Reporting;

namespace Diabetia.API.DTO.ReportingResponse
{
    public class GlucoseResponse
    {
        public int Value { get; set; }
        public string Time { get; set; }

        public static GlucoseResponse FromObject(EventSummary summary)
        {
            var response = new GlucoseResponse
            {
                Time = summary.EventDay.ToString("dd/MM/yyyy"),
                Value = summary.AmountEvents
            };
            return response;
        }
    }
}
