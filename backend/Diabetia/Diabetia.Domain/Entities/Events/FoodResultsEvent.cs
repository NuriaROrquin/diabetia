using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Entities.Events
{
    public class FoodResultsEvent
    {
        public int ChConsumed { get; set; }

        public float InsulinRecomended { get; set; }
    }
}
