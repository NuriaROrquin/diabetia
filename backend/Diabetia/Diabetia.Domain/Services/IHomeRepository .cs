
namespace Diabetia.Domain.Services
{
    public interface IHomeRepository
    {
        public Task GetPhysicalActivity(int idUser, int idEvento);
    }
}
