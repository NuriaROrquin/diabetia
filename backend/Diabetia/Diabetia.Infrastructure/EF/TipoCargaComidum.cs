using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class TipoCargaComidum
    {
        public TipoCargaComidum()
        {
            EventoComida = new HashSet<EventoComidum>();
        }

        public int Id { get; set; }
        public string TipoCarga { get; set; } = null!;

        public virtual ICollection<EventoComidum> EventoComida { get; set; }
    }
}
