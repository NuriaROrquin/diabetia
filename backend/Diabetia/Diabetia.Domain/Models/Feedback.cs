using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int IdCargaEvento { get; set; }
        public int? IdSentimiento { get; set; }
        public DateTime? HoraAviso { get; set; }
        public bool? FueRealizado { get; set; }
        public string? NotaLibre { get; set; }

        public virtual CargaEvento IdCargaEventoNavigation { get; set; } = null!;
        public virtual Sentimiento? IdSentimientoNavigation { get; set; }
    }
}
