
namespace Diabetia.Application.Exceptions
{
    public class PhysicalEventNotRelatedWithEventException : Exception
    {
        public PhysicalEventNotRelatedWithEventException() : base() { }

        public PhysicalEventNotRelatedWithEventException(string message) : base(message) { }

        public PhysicalEventNotRelatedWithEventException(string message, Exception innerException) : base(message, innerException) { }
    }
}
