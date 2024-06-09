
namespace Diabetia.Domain.Services
{
    public interface IHomeRepository
    {
        public Task<int?> GetPhysicalActivity(string email, int idEvento);

        public Task<decimal?> GetChMetrics(string email, int idEvento);

        public Task<int> GetGlucose(string email, int idEvento);

        public Task<int> GetHypoglycemia(string email);

        public Task<int> GetHyperglycemia(string email);

        public Task<int?> GetInsulin(string email, int idEvento);
    }
}
