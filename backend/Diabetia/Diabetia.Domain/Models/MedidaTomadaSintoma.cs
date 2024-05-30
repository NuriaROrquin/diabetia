using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Model
{
    public partial class MedidaTomadaSintoma
    {
        public MedidaTomadaSintoma()
        {
            MedidaTomadaEventoSaluds = new HashSet<MedidaTomadaEventoSalud>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<MedidaTomadaEventoSalud> MedidaTomadaEventoSaluds { get; set; }
    }
}
