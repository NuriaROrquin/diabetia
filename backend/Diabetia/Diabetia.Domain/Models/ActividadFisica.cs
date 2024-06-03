using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class ActividadFisica
    {
        public ActividadFisica()
        {
            EventoActividadFisicas = new HashSet<EventoActividadFisica>();
            PacienteActividadFisicas = new HashSet<PacienteActividadFisica>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<EventoActividadFisica> EventoActividadFisicas { get; set; }
        public virtual ICollection<PacienteActividadFisica> PacienteActividadFisicas { get; set; }
    }
}
