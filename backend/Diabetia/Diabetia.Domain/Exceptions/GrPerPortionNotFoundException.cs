using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Exceptions
{
    public class GrPerPortionNotFoundException : Exception
    {
        public GrPerPortionNotFoundException() : base() { }

        public GrPerPortionNotFoundException(string message) : base(message) { }

        public GrPerPortionNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
