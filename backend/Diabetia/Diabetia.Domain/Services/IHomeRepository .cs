
namespace Diabetia.Domain.Services
{
    public interface IHomeRepository
    {
        public Task<int?> GetPhysicalActivity(string email, int idEvento, int Timelapse);

        public Task<decimal?> GetChMetrics(string email, int idEvento, int Timelapse);

        public Task<int> GetGlucose(string email, int idEvento, int Timelapse);

        public Task<int> GetHypoglycemia(string email, int Timelapse);

        public Task<int> GetHyperglycemia(string email, int Timelapse);

        public Task<int?> GetInsulin(string email, int idEvento, int Timelapse);
    }
}
