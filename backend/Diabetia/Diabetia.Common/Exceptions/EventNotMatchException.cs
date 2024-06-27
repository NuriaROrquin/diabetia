namespace Diabetia.Common.Exceptions
{
    public class EventNotMatchException : Exception
    {
        public EventNotMatchException() : base() { }
        public EventNotMatchException(string message) : base(message) { }
        public EventNotMatchException(string message, Exception innerException) : base(message, innerException) { }
    }
}