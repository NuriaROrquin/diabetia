
namespace Diabetia.Domain.Utilities.Interfaces
{
    public interface IUserStatusValidator
    {
        public Task CheckUserStatus(string email);
    }
}
