namespace Diabetia.Application.Exceptions
{
    public class UserEventNotFoundException : Exception
    {
        public UserEventNotFoundException() : base() { }

        public UserEventNotFoundException(string message) : base(message) { }

        public UserEventNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        
    }
}
