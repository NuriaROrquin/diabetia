using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.Exceptions
{
    public class CantCreatObjectS3Async : Exception
    {
        public CantCreatObjectS3Async() : base() { }

        public CantCreatObjectS3Async(string message) : base(message) { }

        public CantCreatObjectS3Async(string message, Exception innerException) : base(message, innerException) { }
    }
}
