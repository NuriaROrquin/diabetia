using Diabetia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Repositories
{
    public interface IReportingRepository
    {
        public Task<List<EventoInsulina>> GetInsulinEventsToReportByPatientId(int patientId, DateTime dateFrom, DateTime dateTo);
    }
}
