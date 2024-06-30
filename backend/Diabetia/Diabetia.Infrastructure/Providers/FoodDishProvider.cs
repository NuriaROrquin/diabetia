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
            //TODO: traer el create image stream acá
            
            var logMealToken = _configuration["LogMealToken"];
            
            //TODO: El api service deberia devolver el dto de logmeal, y acá mapear a entity de domain 
            var foodDish = await _apiService.DetectFoodDish($"Bearer {logMealToken}", imageStream);

            return foodDish;
        }

        public async Task<IngredientsDetected> GetNutrientPerIngredient(FoodDish foodDish)
        {
            var logMealToken = _configuration["LogMealToken"];
        
            //var confirmedDish = null;
            var imageIdRequest = new ImageRequest()
            {
                ImageId = foodDish.ImageId
            };

            var nutrientsDetected = await _apiService.GetNutrientsPerIngredients($"Bearer {logMealToken}", imageIdRequest);  
            
            var ingredientsDetected = new IngredientsDetected()
            {
                Ingredients = new List<IngredientsRecognized>()
            };
            
            if (nutrientsDetected.NutritionalInfoPerItem != null && nutrientsDetected.NutritionalInfoPerItem.Count > 0)
            {
                var foodName = nutrientsDetected.FoodName;
                foreach (var item in nutrientsDetected.NutritionalInfoPerItem)
                {
                    var ingredient = new IngredientsRecognized()
                    {
                        CarbohydratesPerPortion = item.NutritionalInfo.TotalNutrients["CHOCDF"].Quantity,
                        GrPerPortion = item.ServingSize,
                        FoodItemPosition = item.FoodItemPosition,
                        Name = foodName
                    };

                    ingredientsDetected.Ingredients.Add(ingredient);
                }
            }
            
            return ingredientsDetected;
        }
    }
}
