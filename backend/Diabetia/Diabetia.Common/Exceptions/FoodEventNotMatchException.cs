using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Common.Exceptions
{
    public class FoodEventNotMatchException : Exception
    {
        public FoodEventNotMatchException() : base() { }

        public FoodEventNotMatchException(string message) : base(message) { }

        public FoodEventNotMatchException(string message, Exception innerException) : base(message, innerException) { }
    }
}
