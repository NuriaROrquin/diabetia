using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Entities
{
    public class Calendar
    {
        public Dictionary<string, List<EventItem>> EventList { get; set; }

        public Calendar()
        {
            EventList = new Dictionary<string, List<EventItem>>();
        }
    }

    public class EventItem
    {
        public string Time { get; set; }
        public string Title { get; set; }
        public string Ingredients { get; set; }

    }
}
