namespace Diabetia.API.DTO.EventRequest.Insuline
{
    public class EventInsulinRequest : BasicEventRequest
    {
        public int? IdEvent { get; set; }
        public int? IdKindEvent { get; set; }
        public string? FreeNote { get; set; }
        public int? Insulin { get; set; }
    }
}