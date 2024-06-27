using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Exceptions
{
    public class ChPerPortionNotFoundException : Exception
    {
        public ChPerPortionNotFoundException() : base() { }

        public ChPerPortionNotFoundException(string message) : base(message) { }

        public ChPerPortionNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
