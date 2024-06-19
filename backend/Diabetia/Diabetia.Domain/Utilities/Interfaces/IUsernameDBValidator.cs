
namespace Diabetia.Domain.Utilities.Interfaces
{
    public interface IUsernameDBValidator
    {
        public Task <string> GetUsernameByEmail(string email);

        public Task CheckUsernameOnDataBase(string username);
    }
}
