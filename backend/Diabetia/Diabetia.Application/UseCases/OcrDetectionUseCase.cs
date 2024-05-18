using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Diabetia.Application.UseCases
{
     public class OcrDetectionUseCase
    {
        private readonly IApiAmazonService _apiAmazonService;

        public OcrDetectionUseCase(IApiAmazonService apiAmazonService)
        {
            _apiAmazonService = apiAmazonService;
        }

        public async Task<string> GetOcrResponseFromDocument(string ocrRequest)
        {
            return await _apiAmazonService.GetChFromDocument(ocrRequest);             
        }
    }
}
