using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Model
{
    public partial class Recordatorio
    {
        public Recordatorio()
        {
            RecordatorioDia = new HashSet<RecordatorioDium>();
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
        public virtual ICollection<RecordatorioDium> RecordatorioDia { get; set; }
    }
}
