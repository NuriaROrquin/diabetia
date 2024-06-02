using Diabetia.Domain.Entities;

namespace Diabetia.API
{

    public class CalendarResponse
    {
        public Dictionary<string, List<EventItem>> EventList { get; set; }

        public CalendarResponse()
        {
            EventList = new Dictionary<string, List<EventItem>>();
        }
    }
}
