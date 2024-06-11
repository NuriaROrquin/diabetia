using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.Exceptions
{
    public class ExaminationEventNotFoundException : Exception
    {
        public ExaminationEventNotFoundException() : base() { }

        public ExaminationEventNotFoundException(string message) : base(message) { }

        public ExaminationEventNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
