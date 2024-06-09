namespace Diabetia.API.DTO.EventRequest
{
    public class GlucoseEventRequest : EventRequest
    {
        public int? IdEvent { get; set; }
        public int IdKindEvent { get; set; }
        public string? FreeNote { get; set; }
        public decimal? Glucose { get; set; }
        public int? IdDevicePacient { get; set; }
        public int? IdFoodEvent { get; set; }

        public bool? PostFoodMedition { get; set; }
    }
}