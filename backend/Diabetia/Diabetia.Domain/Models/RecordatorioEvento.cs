using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class RecordatorioEvento
    {
        public int Id { get; set; }
        public int IdRecordatorio { get; set; }
        public int IdDiaSemana { get; set; }
        public DateTime FechaHoraRecordatorio { get; set; }
        public int IdCargaEvento { get; set; }

        public virtual CargaEvento IdCargaEventoNavigation { get; set; } = null!;
        public virtual DiaSemana IdDiaSemanaNavigation { get; set; } = null!;
        public virtual Recordatorio IdRecordatorioNavigation { get; set; } = null!;
    }
}
