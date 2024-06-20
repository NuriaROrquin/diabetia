﻿
namespace Diabetia.Common.Exceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException() : base() { }

        public PatientNotFoundException(string message) : base(message) { }

        public PatientNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
