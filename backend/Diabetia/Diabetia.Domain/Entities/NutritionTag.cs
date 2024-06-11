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
        public float Portion { get; set; }   
        public float GrPerPortion { get; set; }
        public float ChInPortion { get; set; }
        public float ChCalculated { get; set; }

        public string UniqueId { get; set; }

    }
}
