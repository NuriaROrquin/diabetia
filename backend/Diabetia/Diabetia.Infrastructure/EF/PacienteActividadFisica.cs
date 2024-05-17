using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class PacienteActividadFisica
    {
        public PacienteActividadFisica()
        {
            EventoActividadFisicas = new HashSet<EventoActividadFisica>();
        }

        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdActividadFisica { get; set; }
        public int Frecuencia { get; set; }
        public int Duracion { get; set; }

        public virtual ActividadFisica IdActividadFisicaNavigation { get; set; } = null!;
        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
        public virtual ICollection<EventoActividadFisica> EventoActividadFisicas { get; set; }
    }
}
