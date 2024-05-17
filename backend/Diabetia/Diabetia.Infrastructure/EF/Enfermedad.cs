using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class Enfermedad
    {
        public Enfermedad()
        {
            EventoSaluds = new HashSet<EventoSalud>();
            PacienteEnfermedadPreexistentes = new HashSet<PacienteEnfermedadPreexistente>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool EsCronica { get; set; }

        public virtual ICollection<EventoSalud> EventoSaluds { get; set; }
        public virtual ICollection<PacienteEnfermedadPreexistente> PacienteEnfermedadPreexistentes { get; set; }
    }
}
