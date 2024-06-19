
namespace Diabetia.Common.Utilities.Interfaces
{
    public interface IUserStatusValidator
    {
        public Task checkUserStatus(string email);
    }
}
