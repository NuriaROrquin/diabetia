

namespace Diabetia.Domain.Utilities.Interfaces
{
    public interface IEmailDBValidator
    {
        public Task CheckEmailOnDB(string email);
    }
}
