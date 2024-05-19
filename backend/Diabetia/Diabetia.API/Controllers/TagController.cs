using Microsoft.AspNetCore.Mvc;
using Diabetia.Application.UseCases;
using Diabetia.API.DTO;
using Diabetia.Domain.Entities;
using Amazon.Runtime.Internal;


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
        public async Task<IEnumerable<TagDetectionResponse>> GetOcrResponseAsync([FromBody] IEnumerable<TagDetectionRequest> tags)
        {
            List<TagDetectionResponse> responses = new List<TagDetectionResponse>();

            foreach (var tag in tags)
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

        [HttpPost("tagRegistration")]
        public async Task<IEnumerable<TagRegistrationResponse>> ConfirmTagRegistration([FromBody] IEnumerable<TagRegistrationRequest> tags)
        {
            List<TagRegistrationResponse> responses = new List<TagRegistrationResponse>();

            foreach (var tag in tags)
            {
                NutritionTag tagConfirmationRequest = new NutritionTag();

                tagConfirmationRequest.chInPortion = tag.ChInPortion;
                tagConfirmationRequest.grPerPortion = tag.grPerPortion;
                tagConfirmationRequest.portion = tag.Portion;

                float consumed = await _TagCalculateUseCase.GetChPerPortionConsumed(tagConfirmationRequest);

                TagRegistrationResponse tagRegistrationResponse = new TagRegistrationResponse();
                tagRegistrationResponse.Id = tag.Id;
                tagRegistrationResponse.Portion = tag.Portion;
                tagRegistrationResponse.GrPerPortion = tag.grPerPortion;
                tagRegistrationResponse.ChInPortion = tag.ChInPortion;
                tagRegistrationResponse.ChCalculated = consumed;

                responses.Add(tagRegistrationResponse);
            }

            return responses;
        }

    }
}
