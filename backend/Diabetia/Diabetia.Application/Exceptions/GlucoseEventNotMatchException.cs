using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.Exceptions
{
    public class GlucoseEventNotMatchException : Exception
    {
        public GlucoseEventNotMatchException() : base() { }

        public GlucoseEventNotMatchException(string message) : base(message) { }

        public GlucoseEventNotMatchException(string message, Exception innerException) : base(message, innerException) { }
    }

}
