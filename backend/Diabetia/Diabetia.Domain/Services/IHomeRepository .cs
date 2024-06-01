
namespace Diabetia.Domain.Services
{
    public interface IHomeRepository
    {
        public Task<int> GetPhysicalActivity(string Email, int IdEvento);

        public Task<int> GetChMetrics(string Email, int IdEvento);

        public Task<int> GetGlucose(string Email, int IdEvento);

        public Task<int> GetHypoglycemia(string Email);

        public Task<int> GetHyperglycemia(string Email);

        public Task<int> GetInsulin(string Email, int IdEvento);
    }
}
