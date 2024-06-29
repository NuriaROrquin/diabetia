using Diabetia.Domain.Entities;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Services;
using Microsoft.Extensions.Configuration;
using Refit;

namespace Diabetia.Application.UseCases
{
    public class FoodDishDetectionUseCase
    {
        private readonly IConfiguration _configuration;
        private readonly IApiService _apiService;

        public FoodDishDetectionUseCase(IApiService apiService, IConfiguration configuration)
        {
            _apiService = apiService;
            _configuration = configuration;
        }
        
        public async Task<FoodDish> DetectFoodDish(FoodDish foodImageBase64)
        {
            var logMealToken = _configuration["LogMealToken"];
            
            byte[] imageBytes = Convert.FromBase64String(foodImageBase64.ImageBase64);
        
            using var imageStream = new MemoryStream(imageBytes);

            var streamPart = new StreamPart(imageStream, "image.jpg", "image/jpeg");
                
            var foodDish = await _apiService.DetectFoodDish($"Bearer {logMealToken}", streamPart);

            return foodDish;
           
        }
    }
}
