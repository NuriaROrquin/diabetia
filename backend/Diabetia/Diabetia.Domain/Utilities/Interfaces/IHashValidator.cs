
namespace Diabetia.Domain.Utilities.Interfaces
{
    public interface IHashValidator
    {
        public Task<string> GetUserHash(string email);
    }
}
