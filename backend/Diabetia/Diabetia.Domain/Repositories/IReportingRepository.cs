using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Models;

namespace Diabetia.Domain.Repositories
{
    public interface IReportingRepository
    {
        public Task<List<EventoInsulina>> GetInsulinEventsToReportByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);

        public Task<List<PhysicalActivitySummary>> GetAmountPhysicalEventsToReportByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);
    }
}
