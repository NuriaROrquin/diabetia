using Diabetia.Application.UseCases;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Mvc;
using Diabetia.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Diabetia.API.DTO.FoodDish;




namespace Diabetia.API.Controllers.FoodDetection
{
    [ApiController]
    [Route("[controller]")]
    public class FoodDishDetectionController : ControllerBase
    {
        private readonly FoodDishDetectionUseCase _foodDetectionUseCase;
        private readonly FoodManuallyUseCase _foodManuallyUseCase;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public FoodDishDetectionController(FoodDishDetectionUseCase foodDetectionUseCase, FoodManuallyUseCase foodManuallyUseCase, IHttpContextAccessor httpContextAccessor)
        {
            _foodDetectionUseCase = foodDetectionUseCase;
            _foodManuallyUseCase = foodManuallyUseCase;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("foodDetection")]
        public async Task<FoodDishDetectionResponse> GetFoodDetection([FromBody] FoodDishDetectionRequest foodDetectionRequest)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            FoodDish foodDish = new FoodDish();

            foodDish.ImageBase64 = foodDetectionRequest.ImageBase64;

            var detectedFoodDish = await _foodDetectionUseCase.DetectFoodDish(foodDish);

            var segmentationResults = detectedFoodDish.SegmentationResults
               .Select(sr => new DTO.FoodDish.SegmentationResult
               {
                   FoodItemPosition = sr.FoodItemPosition,
                   RecognitionResults = sr.RecognitionResults.Select(rr => new DTO.FoodDish.RecognitionResult
                   {
                       Id = rr.Id,
                       Name = rr.Name,
                       Prob = rr.Prob
                   }).ToList()
               }).ToList();

            var response = new FoodDishDetectionResponse
            {
                ImageId = detectedFoodDish.ImageId,
                SegmentationResults = segmentationResults,
            };
            return response;
        }
    }
}
