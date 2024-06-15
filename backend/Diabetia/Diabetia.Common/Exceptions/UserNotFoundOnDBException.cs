namespace Diabetia.Application.Exceptions
{
    public class UserNotFoundOnDBException : Exception
    {
        public UserNotFoundOnDBException() : base() { }

        public UserNotFoundOnDBException(string message) : base(message) { }

        public UserNotFoundOnDBException(string message, Exception innerException) : base(message, innerException) { }
    }
}
