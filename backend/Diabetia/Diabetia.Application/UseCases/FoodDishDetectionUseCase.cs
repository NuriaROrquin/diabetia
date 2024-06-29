using Diabetia.Domain.Entities;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Services;
using Microsoft.Extensions.Configuration;
using Refit;

namespace Diabetia.Application.UseCases
{
    public class FoodDishDetectionUseCase
    {
        private readonly IFoodDishProvider _foodDishProvider;

        public FoodDishDetectionUseCase(IFoodDishProvider foodDishProvider)
        {
            _foodDishProvider = foodDishProvider;
        }

        public async Task<FoodDish> DetectFoodDish(FoodDish foodImageBase64)
        {
            
            byte[] imageBytes = Convert.FromBase64String(foodImageBase64.ImageBase64);
        
            using var imageStream = new MemoryStream(imageBytes);

            var streamPart = new StreamPart(imageStream, "image.jpg", "image/jpeg");

            var foodDish = await _foodDishProvider.DetectFoodDish(streamPart);

            return foodDish;
           
        }

        public async Task<IngredientsDetected> ConfirmDish(FoodDish foodDish)
        {
            var nutrientsDetected = await _foodDishProvider.GetNutrientPerIngredient(foodDish);

            return nutrientsDetected;

        }
    }
}
