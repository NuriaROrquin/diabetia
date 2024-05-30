using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Model
{
    public partial class PacienteEnfermedadPreexistente
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdEnfermedad { get; set; }

        public virtual Enfermedad IdEnfermedadNavigation { get; set; } = null!;
        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
    }
}
