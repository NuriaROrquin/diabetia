using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Common.Exceptions
{
    public class PatientInsulinRelationNotFoundException : Exception
    {
        public PatientInsulinRelationNotFoundException() : base() { }

        public PatientInsulinRelationNotFoundException(string message) : base(message) { }

        public PatientInsulinRelationNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
