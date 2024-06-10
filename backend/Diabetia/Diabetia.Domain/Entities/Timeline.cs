using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Entities
{
    public class Timeline
    {
        public List<TimelineItem> Items { get; set; }
    }

    public class TimelineItem
    {
        public string Time { get; set; }
        public string Title { get; set; }
    }
}
