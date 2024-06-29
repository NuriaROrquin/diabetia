using Microsoft.AspNetCore.Mvc;
using Diabetia.Application.UseCases;
using Diabetia.API.DTO;
using Diabetia.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Diabetia.Application.UseCases.EventUseCases;
using System.Security.Claims;
using Diabetia.API.DTO.TagResponse;
using Diabetia.API.DTO.TagRequestFromBody;
using Diabetia.API.DTO.EventResponse.Food;

namespace Diabetia.API.Controllers.Tag
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly TagDetectionUseCase _tagDetectionUseCase;
        private readonly TagConfirmationUseCase _tagCalculateUseCase;
            private readonly FoodManuallyUseCase _eventFoodUseCase;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TagController(TagDetectionUseCase tagDetectionUseCase, TagConfirmationUseCase tagCalculateUseCase, FoodManuallyUseCase eventFoodUseCase, IHttpContextAccessor httpContextAccessor)
        {
            _tagDetectionUseCase = tagDetectionUseCase;
            _tagCalculateUseCase = tagCalculateUseCase;
            _eventFoodUseCase = eventFoodUseCase;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("tagDetection")]
        [Authorize]
        public async Task<IEnumerable<TagDetectionResponse>> GetOcrResponseAsync([FromBody] IEnumerable<TagDetectionRequest> tags)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var tagImages = tags.Select(tag => tag.ImageBase64).ToList();
            var tagResponses = await _tagDetectionUseCase.GetOcrResponseFromDocument(email, tagImages);

            var responses = tagResponses.Select((tagResponse, index) => new TagDetectionResponse
            {
                Id = tags.ElementAt(index).Id,
                GrPerPortion = tagResponse.GrPerPortion,
                Portion = tags.ElementAt(index).Portion,
                ChInPortion = tagResponse.ChInPortion,
                UniqueIdTag = tagResponse.UniqueId
            }).ToList();

            return responses;
        }

        [HttpPost("tagRegistration")]
        [Authorize]
        public async Task<FoodResponse> ConfirmTagRegistration([FromBody] TagRegistrationRequest tagsRequest)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            List<NutritionTag> nutritionTags = tagsRequest.ToDomain();

            var foodCalculated = await _tagCalculateUseCase.CalculateFoodResponseAsync(email, nutritionTags);

            await _eventFoodUseCase.AddFoodByTagEvent(email, tagsRequest.EventDate, foodCalculated.ChConsumed);

            FoodResponse responses = new FoodResponse();

            responses.ChConsumed = foodCalculated.ChConsumed;
            responses.InsulinRecomended = foodCalculated.InsulinRecomended;

            return responses;
        }

    }
}
