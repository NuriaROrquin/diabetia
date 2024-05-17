using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class SensibilidadInsulina
    {
        public SensibilidadInsulina()
        {
            Pacientes = new HashSet<Paciente>();
        }

        public int Id { get; set; }
        public string Nivel { get; set; } = null!;

        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
