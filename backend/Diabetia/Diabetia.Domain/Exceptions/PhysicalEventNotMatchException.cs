
namespace Diabetia.Domain.Exceptions
{
    public class PhysicalEventNotMatchException : Exception
    {
        public PhysicalEventNotMatchException() : base() { }

        public PhysicalEventNotMatchException(string message) : base(message) { }

        public PhysicalEventNotMatchException(string message, Exception innerException) : base(message, innerException) { }
    }
}
