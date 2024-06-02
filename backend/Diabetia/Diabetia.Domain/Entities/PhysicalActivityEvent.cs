using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Entities
{
    public class PhysicalActivityEvent
    {
        public int IdEvent { get; set; }
        public int IdEventType { get; set; }
        public int IdPhysicalEducationEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
    }
}
