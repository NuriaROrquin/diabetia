using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diabetia.Domain.Services;
using Diabetia.Domain.Entities;

namespace Diabetia.Application.UseCases
{
    public class OcrCalculateUseCase
    {
        public async Task<string> GetChPerPortionConsumed(NutritionTag nutritionTag) 
        {
            string chConsumed = "";

            float chPerPortionConsumed = (nutritionTag.chInPortion * nutritionTag.portion);

            chConsumed += "Consumiste " + chPerPortionConsumed.ToString() + " carbohidatros en total.";

            return chConsumed;
        }
    }
}
