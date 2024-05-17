using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class TipoDiabete
    {
        public TipoDiabete()
        {
            Pacientes = new HashSet<Paciente>();
        }

        public int Id { get; set; }
        public string? Tipo { get; set; }

        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
