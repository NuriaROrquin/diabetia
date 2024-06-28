using Diabetia.Domain.Entities.Reporting;

namespace Diabetia.API.DTO.ReportingResponse.PhysicalActivity
{
    public class PhysicalActivityAmountResponse
    {
        public int Value { get; set; }
        public string Time { get; set; }

        public static PhysicalActivityAmountResponse FromObject(EventSummary summary)
        {
            var response = new PhysicalActivityAmountResponse
            {
                Time = summary.EventDay.ToString("dd/MM/yyyy"),
                Value = summary.AmountEvents
            };
            return response;
        }
    }
}
