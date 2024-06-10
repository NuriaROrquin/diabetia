using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class GravedadSintoma
    {
        public GravedadSintoma()
        {
            SintomaEventoSaluds = new HashSet<SintomaEventoSalud>();
        }

        public int Id { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<SintomaEventoSalud> SintomaEventoSaluds { get; set; }
    }
}
