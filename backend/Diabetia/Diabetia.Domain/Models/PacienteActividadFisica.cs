using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class PacienteActividadFisica
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdActividadFisica { get; set; }
        public int Frecuencia { get; set; }
        public int Duracion { get; set; }

        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
    }
}
