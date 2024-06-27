
namespace Diabetia.Common.Exceptions
{
    public class EventNotRelatedWithPatientException : Exception
    {
        public EventNotRelatedWithPatientException() : base() { }

        public EventNotRelatedWithPatientException(string message) : base(message) { }

        public EventNotRelatedWithPatientException(string message, Exception innerException) : base(message, innerException) { }

    }
}
