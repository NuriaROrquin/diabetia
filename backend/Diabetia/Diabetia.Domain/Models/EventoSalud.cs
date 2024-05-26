using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class EventoSalud
    {
        public EventoSalud()
        {
            MedidaTomadaEventoSaluds = new HashSet<MedidaTomadaEventoSalud>();
            SintomaEventoSaluds = new HashSet<SintomaEventoSalud>();
        }

        public int Id { get; set; }
        public int IdCargaEvento { get; set; }
        public int IdEnfermedad { get; set; }

        public virtual CargaEvento IdCargaEventoNavigation { get; set; } = null!;
        public virtual Enfermedad IdEnfermedadNavigation { get; set; } = null!;
        public virtual ICollection<MedidaTomadaEventoSalud> MedidaTomadaEventoSaluds { get; set; }
        public virtual ICollection<SintomaEventoSalud> SintomaEventoSaluds { get; set; }
    }
}
