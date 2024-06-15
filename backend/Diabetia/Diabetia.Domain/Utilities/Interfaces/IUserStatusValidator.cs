
namespace Diabetia.Domain.Utilities.Interfaces
{
    public interface IUserStatusValidator
    {
        public Task checkUserStatus(string email);
    }
}
