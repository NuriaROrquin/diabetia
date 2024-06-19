using Diabetia.Domain.Entities;
using Diabetia.Domain.Utilities;

namespace Diabetia.API.DTO.HomeRequest
{
    public class MetricsResponse
    {
        public MetricsResponse(Metrics metrics)
        {
            this.Carbohidrates = new Carbohidrates
            {
                Quantity = metrics.Carbohydrates
            };
            this.PhysicalActivity = new PhysicalActivity
            {
                Quantity = metrics.PhysicalActivity,
                IsWarning = metrics.PhysicalActivity < 30
            };
            this.Glycemia = new Glycemia
            {
                Quantity = metrics.Glycemia,
                IsWarning = metrics.Glycemia < (int)GlucoseEnum.HIPOGLUCEMIA || metrics.Glycemia > (int)GlucoseEnum.HIPERGLUCEMIA
            };
            this.Hypoglycemia = new Hypoglycemia
            {
                Quantity = metrics.Hypoglycemia,
                IsWarning = metrics.Hypoglycemia >= 1
            };
            this.Hyperglycemia = new Hyperglycemia
            {
                Quantity = metrics.Hyperglycemia,
                IsWarning = metrics.Hyperglycemia >= 1
            };
            this.Insulin = new Insulin
            {
                Quantity = metrics.Insulin
            };
        }

        public Carbohidrates Carbohidrates { get; set; }
        public PhysicalActivity PhysicalActivity { get; set; }
        public Hyperglycemia Hyperglycemia { get; set; }
        public Hypoglycemia Hypoglycemia { get; set; }
        public Glycemia Glycemia { get; set; }
        public Insulin Insulin { get; set; }
    }

    public class Carbohidrates : Metric
    {
        public decimal? Quantity { get; set; }
    }

    public class PhysicalActivity : Metric
    {
        public int? Quantity { get; set; }
    }

    public class Hyperglycemia : Metric
    {
        public int Quantity { get; set; }
    }

    public class Hypoglycemia : Metric
    {
        public int Quantity { get; set; }
    }

    public class Glycemia : Metric
    {
        public int Quantity { get; set; }
    }

    public class Insulin : Metric
    {
        public int? Quantity { get; set; }
    }

    public class Metric
    {
        public bool? IsWarning { get; set; }
    }

}
