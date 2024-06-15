namespace Diabetia.Common.Exceptions
{
    public class MismatchUserPatientException : Exception
    {
        public MismatchUserPatientException() : base() { }

        public MismatchUserPatientException(string message) : base(message) { }

        public MismatchUserPatientException(string message, Exception innerException) : base(message, innerException) { }
    }
}
