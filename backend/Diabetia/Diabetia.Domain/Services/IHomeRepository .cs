
using Diabetia.Common.Utilities;
using Diabetia.Domain.Models;

namespace Diabetia.Domain.Services
{
    public interface IHomeRepository
    {
        public Task<int?> GetPhysicalActivity(string email, int idEvento, DateFilter? dateFilter);

        public Task<decimal?> GetChMetrics(string email, int idEvento, DateFilter? dateFilter);

        public Task<int> GetGlucose(string email, int idEvento, DateFilter? dateFilter);

        public Task<int> GetHypoglycemia(string email, DateFilter? dateFilter);

        public Task<int> GetHyperglycemia(string email, DateFilter? dateFilter);

        public Task<int?> GetInsulin(string email, int idEvento, DateFilter? dateFilter);

        public Task<List<CargaEvento>> GetLastEvents(string email);
    }
}
