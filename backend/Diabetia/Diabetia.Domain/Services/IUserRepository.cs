
namespace Diabetia.Domain.Services
{
    public interface IUserRepository
    {

        public async Task CompleteUserInfo(string name, string email, string gender, string lastname, int weight, string phone);
    }
}
