using Microsoft.AspNetCore.Mvc;
using Diabetia.Application.UseCases;
using Diabetia.API.DTO;


namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OcrController : ControllerBase
    {
        private readonly OcrDetectionUseCase _OcrDetectionUseCase;
        public OcrController(OcrDetectionUseCase OcrDetectionUseCase)
        {
            _OcrDetectionUseCase = OcrDetectionUseCase;
        }

        [HttpPost("ocrdetection")]
        public async Task<string> GetOcrResponseAsync([FromBody] OcrRequest request)
        {
            return await _OcrDetectionUseCase.GetOcrResponseFromDocument(request.ImageBase64);

             
        }
    }
}
