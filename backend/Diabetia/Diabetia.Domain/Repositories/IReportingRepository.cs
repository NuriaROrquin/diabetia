using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Models;
using Diabetia.Domain.Utilities;

namespace Diabetia.Domain.Repositories
{
    public interface IReportingRepository
    {
        // -------------------------------------------------------------- ⬇⬇ Insulin Report ⬇⬇ -------------------------------------------------------------------
        public Task<List<EventoInsulina>> GetInsulinEventsToReportByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        public Task<List<EventSummary>> GetInsulinEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        // ---------------------------------------------------------- ⬇⬇ Physical Activity Report ⬇⬇ -------------------------------------------------------------
        public Task<List<EventSummary>> GetPhysicalActivityEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        public Task<List<ActivityDurationSummary>> GetPhysicalActivityEventDurationsByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        // --------------------------------------------------------------- ⬇⬇ Glucose Report ⬇⬇ ------------------------------------------------------------------
        public Task<List<EventSummary>> GetGlucoseEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        public Task<List<GlucoseMeasurement>> GetHyperglycemiaGlucoseHistoryByPatientId(int patientId, GlucoseEnum hyperglycemiaValue);

        public Task<List<GlucoseMeasurement>> GetHypoglycemiaGlucoseHistoryByPatientId(int patientId, GlucoseEnum hypoglycemiaValue);

        // ----------------------------------------------------------------- ⬇⬇ Food Report ⬇⬇ -------------------------------------------------------------------
        public Task<List<EventSummary>> GetFoodEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        
    }
}
