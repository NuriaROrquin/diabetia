namespace Diabetia.API.DTO.EventRequest
{
    public class EventGlucoseRequest : EventRequest
    {
        public int? IdEvent { get; set; }
        public int? IdKindEvent { get; set; }
        public string? FreeNote { get; set; }
        public decimal? Glucose { get; set; }
        public int? IdFoodEvent { get; set; }
        public bool? PostFoodMedition { get; set; }
    }
}