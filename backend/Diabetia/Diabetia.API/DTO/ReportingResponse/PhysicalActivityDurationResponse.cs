using Diabetia.Domain.Entities.Reporting;

namespace Diabetia.API.DTO.ReportingResponse
{
    public class PhysicalActivityDurationResponse
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public static PhysicalActivityDurationResponse FromObject(ActivityDurationSummary summary)
        {
            var response = new PhysicalActivityDurationResponse
            {
                Value = summary.TotalDuration,
                Name = summary.ActivityName
            };
            return response;
        }
    }
}
