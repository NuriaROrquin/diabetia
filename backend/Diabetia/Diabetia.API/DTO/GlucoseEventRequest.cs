namespace Diabetia.API.DTO
{
    public class GlucoseEventRequest
    {
        public int? IdEvent { get; set; }
        public string Email { get; set; }
        public int IdKindEvent { get; set; }
        public DateTime EventDate { get; set; }
        public string? FreeNote { get; set; }
        public decimal Glucose { get; set; }
        public int? IdDevicePacient { get; set; }
        public int? IdFoodEvent { get; set; }

        public bool? PostFoodMedition { get; set; }
    }
}