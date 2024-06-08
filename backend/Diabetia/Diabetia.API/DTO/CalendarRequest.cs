namespace Diabetia.API.DTO
{
    public class CalendarRequest
    {
        public string Email { get; set; }
    }

    public class CalendarRequestByDay : CalendarRequest
    {
        public DateTime Date { get; set; }
    }
}
