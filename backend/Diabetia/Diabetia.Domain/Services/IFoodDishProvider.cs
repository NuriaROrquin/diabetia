using Diabetia.Domain.Entities;
using Refit;


namespace Diabetia.Domain.Services
{
    public interface IFoodDishProvider
    {
        public Task<FoodDish> DetectFoodDish(StreamPart imageStream);
    }
}
