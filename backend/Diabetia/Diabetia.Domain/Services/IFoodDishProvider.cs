using Diabetia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Services
{
    public interface IFoodDishProvider
    {
        public Task<FoodDish> DetectFoodDish(Stream imageStream);
    }
}
