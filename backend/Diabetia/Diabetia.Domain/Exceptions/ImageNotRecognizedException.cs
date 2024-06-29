using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Exceptions
{
    public class ImageNotRecognizedException : Exception
    {
        public ImageNotRecognizedException() : base() { }

        public ImageNotRecognizedException(string message) : base(message) { }

        public ImageNotRecognizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
