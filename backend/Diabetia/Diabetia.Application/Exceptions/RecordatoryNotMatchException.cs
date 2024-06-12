namespace Diabetia.Application.Exceptions
{
    public class RecordatoryNotMatchException : Exception
    {
        public RecordatoryNotMatchException() : base() { }
        public RecordatoryNotMatchException(string message) : base(message) { }
        public RecordatoryNotMatchException(string message, Exception innerException) : base(message, innerException) { }
    }
}