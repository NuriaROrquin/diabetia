
namespace Diabetia.Domain.Utilities.Interfaces
{
    public interface IUsernameDBValidator
    {
        public Task <string> CheckUsernameOnDB(string email);
    }
}
