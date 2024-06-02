namespace Diabetia.API
{
    public class MetricsResponse { 
        public decimal? ChMetrics { get; set; }
        public int? PhysicalActivity { get; set; }
        public int Hyperglycemia { get; set; }
        public int Hypoglycemia { get; set; }
        public int Glycemia { get; set; }
        public int? Insulin { get; set; }
    }

}
