using Diabetia.Domain.Entities;
using Diabetia.Domain.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Refit;


namespace Diabetia.Infrastructure.Providers
{
    public class FoodDishProvider : IFoodDishProvider
    {
        private readonly IApiService _apiService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public FoodDishProvider(HttpClient httpClient, IConfiguration configuration, IApiService apiService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiService = apiService;
        }

        public async Task<FoodDish> DetectFoodDish(StreamPart imageStream)
        {
            var logMealToken = _configuration["LogMealToken"];
            
            var foodDish = await _apiService.DetectFoodDish($"Bearer {logMealToken}", imageStream);

            return foodDish;
        }

        public async Task<IngredientsDetected> GetNutrientPerIngredient(FoodDish foodDish)
        {
            var logMealToken = _configuration["LogMealToken"];

            //var confirmedDish = null;
            var imageId = foodDish.ImageId;

            var nutrientsDetected = await _apiService.GetNutrientsPerIngredients($"Bearer {logMealToken}", imageId);  
            return nutrientsDetected;
        }
    }
}
