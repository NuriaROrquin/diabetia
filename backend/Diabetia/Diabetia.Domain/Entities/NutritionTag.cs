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
        public float portion { get; set; }   
        public float grPerPortion { get; set; }
        public float chInPortion { get; set; }

    }
}
