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

        public Timeline()
        {
            Items = new List<TimelineItem>();
        }
    }

    public class TimelineItem
    {
        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public bool? IsWarning { get; set; }
    }
}
