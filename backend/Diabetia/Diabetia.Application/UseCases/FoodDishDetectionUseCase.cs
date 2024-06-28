using Diabetia.Domain.Entities;
using Diabetia.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

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
