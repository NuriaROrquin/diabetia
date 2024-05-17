using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class VinculoProfesionalPaciente
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdProfesional { get; set; }

        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
        public virtual Profesional IdProfesionalNavigation { get; set; } = null!;
    }
}
