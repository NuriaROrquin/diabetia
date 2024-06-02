using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Entities
{
    public class Metrics
    {
        public decimal? Carbohydrates { get; set; }
        public int? PhysicalActivity { get; set; }
        public int Hyperglycemia { get; set; }
        public int Hypoglycemia { get; set; }
        public int Glycemia { get; set; }
        public int? Insulin { get; set; }
    }
}
