using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Entities
{
    public class NutritionTag
    {
        public string CarbohydratesText { get; set; }
        public float Confidece { get; set; }
        public int portion { get; set; }   

    }
}
