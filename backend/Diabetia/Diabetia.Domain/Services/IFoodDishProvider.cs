using Diabetia.Domain.Entities;


namespace Diabetia.Domain.Services
{
    public interface IFoodDishProvider
    {
        public Task<FoodDish> DetectFoodDish(Stream imageStream);
    }
}
