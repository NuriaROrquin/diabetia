

namespace Diabetia.Common.Utilities.Interfaces
{
    public interface IEmailDBValidator
    {
        public Task CheckEmailOnDB(string email);
    }
}
