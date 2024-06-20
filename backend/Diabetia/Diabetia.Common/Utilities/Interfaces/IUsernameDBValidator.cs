
namespace Diabetia.Common.Utilities.Interfaces
{
    public interface IUsernameDBValidator
    {
        public Task <string> CheckUsernameOnDB(string email);
    }
}
