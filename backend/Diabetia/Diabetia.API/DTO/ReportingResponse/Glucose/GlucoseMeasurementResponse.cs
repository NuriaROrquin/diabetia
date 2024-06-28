using Diabetia.Domain.Entities.Reporting;

namespace Diabetia.API.DTO.ReportingResponse.Glucose
{
    public class GlucoseMeasurementResponse
    {
        public decimal Value { get; set; }
        public string Time { get; set; }

        public static GlucoseMeasurementResponse FromObject(GlucoseMeasurement glucoseMeasurement)
        {
            var response = new GlucoseMeasurementResponse()
            {
                Time = glucoseMeasurement.MeasurementDate.ToString("dd/MM/yyyy"),
                Value = glucoseMeasurement.GlucoseLevel
            };

            return response;
        }
    }
}
