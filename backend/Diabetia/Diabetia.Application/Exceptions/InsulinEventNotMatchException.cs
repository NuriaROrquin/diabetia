using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.Exceptions
{
    public class InsulinEventNotMatchException : Exception
    {
        public InsulinEventNotMatchException() : base() { }

        public InsulinEventNotMatchException(string message) : base(message) { }

        public InsulinEventNotMatchException(string message, Exception innerException) : base(message, innerException) { }
    }
}
