using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class TipoEvento
    {
        public TipoEvento()
        {
            CargaEventos = new HashSet<CargaEvento>();
        }

        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string? NombreTabla { get; set; }
        public int? TiempoFeedback { get; set; }

        public virtual ICollection<CargaEvento> CargaEventos { get; set; }
    }
}
