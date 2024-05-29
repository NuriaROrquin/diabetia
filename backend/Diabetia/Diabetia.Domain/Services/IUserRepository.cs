
namespace Diabetia.Domain.Services
{
    public interface IUserRepository
    {

        public Task CompleteUserInfo(string name, string email, string gender, string lastname, int weight, string phone);
        public Task UpdateUserInfo(int typeDiabetes, bool useInsuline, string typeInsuline, string email);
        public Task<bool> GetInformationCompleted(string username);
        Task CompletePhysicalUserInfo(string email, bool haceActividadFisica, int frecuencia, int idActividadFisica);
    }
}
