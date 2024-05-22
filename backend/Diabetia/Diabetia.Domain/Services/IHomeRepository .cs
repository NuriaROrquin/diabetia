
namespace Diabetia.Domain.Services
{
    public interface IHomeRepository
    {
        public Task<int> GetPhysicalActivity(string Email, int idEvento);

        public Task<int> GetChMetrics(string Email, int idEvento);
    }
}
