using Diabetia.Domain.Entities;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class FoodDishDetectionUseCase
    {
        private readonly IFoodDishProvider _foodDishProvidercs;

        public FoodDishDetectionUseCase(IFoodDishProvider foodDishProvidercs)
        {
            _foodDishProvidercs = foodDishProvidercs;
        }
        public async Task<FoodDish> DetectFoodDish(FoodDish foodImageBase64)
        {
            byte[] imageBytes = Convert.FromBase64String(foodImageBase64.ImageBase64);

            using (var stream = new MemoryStream(imageBytes))
            {
                FoodDish response = new FoodDish();
                return response = await _foodDishProvidercs.DetectFoodDish(stream);
            }
           
        }
    }
}
