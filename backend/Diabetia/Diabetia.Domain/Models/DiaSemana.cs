using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Model
{
    public partial class DiaSemana
    {
        public DiaSemana()
        {
            RecordatorioDia = new HashSet<RecordatorioDium>();
        }

        public int Id { get; set; }
        public string Dia { get; set; } = null!;

        public virtual ICollection<RecordatorioDium> RecordatorioDia { get; set; }
    }
}
