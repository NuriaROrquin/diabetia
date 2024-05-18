using Microsoft.AspNetCore.Mvc;
using Diabetia.Application.UseCases;
using Diabetia.API.DTO;
using Diabetia.Domain.Entities;


namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OcrController : ControllerBase
    {
        private readonly OcrDetectionUseCase _OcrDetectionUseCase;
        private readonly OcrCalculateUseCase _OcrCalculateUseCase;
        public OcrController(OcrDetectionUseCase OcrDetectionUseCase, OcrCalculateUseCase ocrCalculateUseCase)
        {
            _OcrDetectionUseCase = OcrDetectionUseCase;
            _OcrCalculateUseCase = ocrCalculateUseCase;
        }

        [HttpPost("ocrdetection")]
        public async Task<OcrResponse> GetOcrResponseAsync([FromBody] OcrRequest request)
        {
            NutritionTag tagRequest = new NutritionTag();
            tagRequest = await _OcrDetectionUseCase.GetOcrResponseFromDocument(request.ImageBase64);

            tagRequest.portion = request.portion;

            
            string consumed = await _OcrCalculateUseCase.GetChPerPortionConsumed(tagRequest);

            OcrResponse response = new OcrResponse();

            response.grPerPortion = tagRequest.grPerPortion;

            response.portion = request.portion;

            response.chInPortion = tagRequest.chInPortion;

            response.CarbohydratesText = consumed;

            return response;
             
        }
    }
}
