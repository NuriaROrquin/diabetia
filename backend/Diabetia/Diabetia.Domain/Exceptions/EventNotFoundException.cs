
namespace Diabetia.Domain.Exceptions
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException() : base() { }

        public EventNotFoundException(string message) : base(message) { }

        public EventNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
