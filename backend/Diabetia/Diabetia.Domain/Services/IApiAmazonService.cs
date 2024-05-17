using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Services
{
    public interface IApiAmazonService
    {
        public Task<string> GetChFromDocument(string ocrRequest);
        
    }
}
