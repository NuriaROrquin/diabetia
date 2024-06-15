
namespace Diabetia.Common.Exceptions
{
    public class NoInformationUserException : Exception
    {
        public NoInformationUserException() : base() { }

        public NoInformationUserException(string message) : base(message) { }

        public NoInformationUserException(string message, Exception innerException) : base(message, innerException) { }
    }
}
