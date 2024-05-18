using Microsoft.AspNetCore.Mvc;
using Diabetia.Application.UseCases;
using Diabetia.API.DTO;
using Diabetia.Domain.Entities;


namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly TagDetectionUseCase _TagDetectionUseCase;
        private readonly TagCalculateUseCase _TagCalculateUseCase;
        public TagController(TagDetectionUseCase tagDetectionUseCase, TagCalculateUseCase tagCalculateUseCase)
        {
            _TagDetectionUseCase = tagDetectionUseCase;
            _TagCalculateUseCase = tagCalculateUseCase;
        }

        [HttpPost("tagDetection")]
        public async Task<TagDetectionResponse> GetOcrResponseAsync([FromBody] TagDetectionRequest request)
        {
            NutritionTag tagRequest = new NutritionTag();
            tagRequest = await _TagDetectionUseCase.GetOcrResponseFromDocument(request.ImageBase64);

            tagRequest.portion = request.portion;

            TagDetectionResponse response = new TagDetectionResponse();

            response.grPerPortion = tagRequest.grPerPortion;

            response.portion = request.portion;

            response.chInPortion = tagRequest.chInPortion;

            return response;
             
        }

        [HttpPost("FoodTagRegistration")]
        public async Task<TagRegistrationResponse> ConfirmTagRegistration([FromBody] TagRegistrationRequest request)
        {
            NutritionTag tagConfirmationRequest = new NutritionTag();

            tagConfirmationRequest.chInPortion = request.chInPortion;

            tagConfirmationRequest.grPerPortion = request.grPerPortion;

            tagConfirmationRequest.portion = request.portion;

            float consumed = await _TagCalculateUseCase.GetChPerPortionConsumed(tagConfirmationRequest);

            TagRegistrationResponse tagRegistrationResponse = new TagRegistrationResponse();

            tagRegistrationResponse.portion = request.portion;

            tagRegistrationResponse.grPerPortion = request.grPerPortion;

            tagRegistrationResponse.chInPortion = request.chInPortion;

            tagRegistrationResponse.chCalculated = consumed;

            return tagRegistrationResponse;
        }

    }
}
