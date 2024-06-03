namespace Diabetia.API
{
    public class MetricsResponse { 
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
