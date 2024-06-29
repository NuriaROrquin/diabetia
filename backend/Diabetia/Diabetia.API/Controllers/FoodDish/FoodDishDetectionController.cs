﻿using Diabetia.Application.UseCases;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Mvc;
using Diabetia.Domain.Entities;
using System.Security.Claims;
using Diabetia.API.DTO.EventResponse.Food;
using Diabetia.API.DTO.FoodDishResponse;
using Diabetia.API.DTO.FoodDishRequest;
using Microsoft.Extensions.Logging;

namespace Diabetia.API.Controllers.FoodDetection
{
    [ApiController]
    [Route("[controller]")]
    public class FoodDishDetectionController : ControllerBase
    {
        private readonly FoodDishDetectionUseCase _foodDishDetectionUseCase;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public FoodDishDetectionController(FoodDishDetectionUseCase foodDishDetectionUseCase, IHttpContextAccessor httpContextAccessor)
        {
            _foodDishDetectionUseCase = foodDishDetectionUseCase;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("foodDetection")]
        public async Task<FoodDishDetectionResponse> GetFoodDetection([FromBody] FoodDishDetectionRequest foodDetectionRequest)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            FoodDish foodDish = new FoodDish();

            foodDish.ImageBase64 = foodDetectionRequest.ImageBase64;

            var detectedFoodDish = await _foodDishDetectionUseCase.DetectFoodDish(foodDish);

            var segmentationResults = detectedFoodDish.SegmentationResults
               .Select(sr => new DTO.FoodDishResponse.SegmentationResult
               {
                   FoodItemPosition = sr.FoodItemPosition,
                   RecognitionResults = sr.RecognitionResults.Select(rr => new DTO.FoodDishResponse.RecognitionResult
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
        
        
        [HttpPost("confirmIngredients")]
        public async Task<ConfirmIngredientsResponse> ConfirmIngredients([FromBody] ConfirmIngredientsRequest confirmIngredientsRequest)
        {
            var foodDish = confirmIngredientsRequest.ToDomain();

            var nutrienstDetected = await _foodDishDetectionUseCase.ConfirmDish(foodDish);

            var mappedEvents = new ConfirmIngredientsResponse(nutrienstDetected); 
            return mappedEvents;
        }
        
        [HttpPost("confirmQuantity")]
        public async Task<FoodResponse> ConfirmQuantity([FromBody] ConfirmQuantityRequest confirmIngredientsRequest)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            List<FoodInfo> ingredientsConfirmed = confirmIngredientsRequest.ToDomain();

            var foodCalculated = await _foodDishDetectionUseCase.SaveFoodEvent(email, ingredientsConfirmed);

            var foodResponse = new FoodResponse
            {
                ChConsumed = foodCalculated.ChConsumed,
                InsulinRecomended = foodCalculated.InsulinRecomended
            };

            return foodResponse;
        }
    }
}