using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Models;

namespace Diabetia.Domain.Repositories
{
    public interface IReportingRepository
    {
        public Task<List<EventoInsulina>> GetInsulinEventsToReportByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        public Task<List<EventSummary>> GetPhysicalActivityEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        public Task<List<EventSummary>> GetGlucoseEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        public Task<List<EventSummary>> GetInsulinEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        public Task<List<EventSummary>> GetFoodEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        public Task<List<ActivityDurationSummary>> GetPhysicalActivityEventDurationsByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);
    }
}
