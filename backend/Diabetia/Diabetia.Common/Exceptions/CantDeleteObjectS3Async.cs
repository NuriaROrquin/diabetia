
namespace Diabetia.Common.Exceptions
{
    public class CantDeleteObjectS3Async : Exception
    {
        public CantDeleteObjectS3Async() : base() { }

        public CantDeleteObjectS3Async(string message) : base(message) { }

        public CantDeleteObjectS3Async(string message, Exception innerException) : base(message, innerException) { }
    }
}
