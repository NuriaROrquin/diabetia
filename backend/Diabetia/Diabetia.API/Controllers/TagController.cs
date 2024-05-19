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
        public async Task<IEnumerable<TagDetectionResponse>> GetOcrResponseAsync([FromBody] IEnumerable<TagDetectionRequest> request)
        {
            List<TagDetectionResponse> responses = new List<TagDetectionResponse>();

            foreach (var tag in request)
            {
                var tagResponses = await _TagDetectionUseCase.GetOcrResponseFromDocument(new List<string> { tag.ImageBase64 });

                foreach (var tagResponse in tagResponses)
                {
                    responses.Add(new TagDetectionResponse
                    {
                        Id = tag.Id,
                        GrPerPortion = tagResponse.grPerPortion,
                        Portion = tag.Portion,
                        ChInPortion = tagResponse.chInPortion
                    });
                }
            }

            return responses;
        }

        [HttpPost("foodTagRegistration")]
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
