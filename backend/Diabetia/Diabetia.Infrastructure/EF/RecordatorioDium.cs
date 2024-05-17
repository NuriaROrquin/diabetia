using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class RecordatorioDium
    {
        public int Id { get; set; }
        public int IdRecordatorio { get; set; }
        public int IdDiaSemana { get; set; }
        public DateTime FechaHoraRecordatorio { get; set; }

        public virtual DiaSemana IdDiaSemanaNavigation { get; set; } = null!;
        public virtual Recordatorio IdRecordatorioNavigation { get; set; } = null!;
    }
}
