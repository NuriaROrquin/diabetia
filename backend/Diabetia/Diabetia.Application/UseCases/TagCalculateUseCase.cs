using Diabetia.Domain.Entities;

namespace Diabetia.Application.UseCases
{
    public class TagCalculateUseCase
    {
        public async Task<float> GetChPerPortionConsumed(NutritionTag nutritionTag) 
        {

            float chPerPortionConsumed = (nutritionTag.ChInPortion * nutritionTag.Portion);

            return chPerPortionConsumed;
        }
    }
}
