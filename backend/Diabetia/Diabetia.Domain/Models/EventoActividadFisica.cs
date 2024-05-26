using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class EventoActividadFisica
    {
        public int Id { get; set; }
        public int IdCargaEvento { get; set; }
        public int? IdActividadRegistrada { get; set; }
        public int? Duracion { get; set; }

        public virtual PacienteActividadFisica? IdActividadRegistradaNavigation { get; set; }
        public virtual CargaEvento IdCargaEventoNavigation { get; set; } = null!;
    }
}
