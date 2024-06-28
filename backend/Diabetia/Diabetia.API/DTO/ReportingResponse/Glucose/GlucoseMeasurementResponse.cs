using Diabetia.Domain.Entities.Reporting;

namespace Diabetia.API.DTO.ReportingResponse.Glucose
{
    public class GlucoseMeasurementResponse
    {
        public int Value { get; set; }
        public DateTime Time { get; set; }

        public static GlucoseMeasurementResponse FromObject(GlucoseMeasurement glucoseMeasurement)
        {
            var response = new GlucoseMeasurementResponse()
            {
                Time = glucoseMeasurement.MeasurementDate,
                Value = glucoseMeasurement.GlucoseLevel
            };

            return response;
        }
    }
}
