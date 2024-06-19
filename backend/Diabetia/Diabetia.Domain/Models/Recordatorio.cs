using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class Recordatorio
    {
        public Recordatorio()
        {
            RecordatorioEventos = new HashSet<RecordatorioEvento>();
        }

        public int Id { get; set; }
        public int IdTipoEvento { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly? FechaFinalizacion { get; set; }
        public TimeOnly? HorarioActividad { get; set; }
        public bool? EstaActivo { get; set; }
        public bool? FueEliminado { get; set; }
        public DateTime? FechaEliminacion { get; set; }

        public virtual TipoEvento IdTipoEventoNavigation { get; set; } = null!;
        public virtual ICollection<RecordatorioEvento> RecordatorioEventos { get; set; }
    }
}
