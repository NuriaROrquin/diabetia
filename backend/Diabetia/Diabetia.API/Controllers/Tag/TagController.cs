using Microsoft.AspNetCore.Mvc;
using Diabetia.Application.UseCases;
using Diabetia.API.DTO;
using Diabetia.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Diabetia.API.Controllers.Tag
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly TagDetectionUseCase _tagDetectionUseCase;
        private readonly TagCalculateUseCase _tagCalculateUseCase;
        private readonly DataUserUseCase _dataUserUseCase;
        private readonly EventFoodUseCase _eventFoodUseCase;
        public TagController(TagDetectionUseCase tagDetectionUseCase, TagCalculateUseCase tagCalculateUseCase, DataUserUseCase dataUserUseCase, EventFoodUseCase eventFoodUseCase)
        {
            _tagDetectionUseCase = tagDetectionUseCase;
            _tagCalculateUseCase = tagCalculateUseCase;
            _dataUserUseCase = dataUserUseCase;
            _eventFoodUseCase = eventFoodUseCase;
        }

        [HttpPost("tagDetection")]
        [Authorize]
        public async Task<IEnumerable<TagDetectionResponse>> GetOcrResponseAsync([FromBody] IEnumerable<TagDetectionRequest> tags)
        {
            List<TagDetectionResponse> responses = new List<TagDetectionResponse>();

            foreach (var tag in tags)
            {
                var tagResponses = await _tagDetectionUseCase.GetOcrResponseFromDocument(new List<string> { tag.ImageBase64 });

                foreach (var tagResponse in tagResponses)
                {
                    responses.Add(new TagDetectionResponse
                    {
                        Id = tag.Id,
                        GrPerPortion = tagResponse.GrPerPortion,
                        Portion = tag.Portion,
                        ChInPortion = tagResponse.ChInPortion,
                        UniqueIdTag = tagResponse.UniqueId
                    });
                }
            }

            return responses;
        }

        [HttpPost("tagRegistration")]
        [Authorize]
        public async Task<TagRegistrationResponse> ConfirmTagRegistration([FromBody] TagRegistrationRequest tagsRequest)
        {
            TagRegistrationResponse responses = new TagRegistrationResponse();
            float totalChConsumed = 0;

            foreach (var tag in tagsRequest.Tags)
            {
                NutritionTag tagConfirmationRequest = new NutritionTag()
                {
                    ChInPortion = tag.ChInPortion,
                    GrPerPortion = tag.GrPerPortion,
                    Portion = tag.Portion,
                };


                float consumed = await _tagCalculateUseCase.GetChPerPortionConsumed(tagConfirmationRequest);
                totalChConsumed += consumed;

                responses.Tags.Add(new ResponsePerTag
                {
                    Id = tag.Id,
                    Portion = tag.Portion,
                    GrPerPortion = tag.GrPerPortion,
                    ChInPortion = tag.ChInPortion,
                    ChCalculated = consumed,
                });
            }

            var userPatientInfo = await _dataUserUseCase.GetPatientInfo(tagsRequest.Email);

            float insulinToCorrect = (float)(totalChConsumed / userPatientInfo.ChCorrection);

            responses.ChConsumed = (int)totalChConsumed;
            responses.InsulinToCorrect = (float)Math.Round(insulinToCorrect, 2);

            await _eventFoodUseCase.AddFoodByTagEvent(tagsRequest.Email, tagsRequest.EventDate, responses.ChConsumed);

            return responses;
        }

    }
}
