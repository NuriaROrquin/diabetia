using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Model
{
    public partial class ActividadFisica
    {
        public ActividadFisica()
        {
            PacienteActividadFisicas = new HashSet<PacienteActividadFisica>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<PacienteActividadFisica> PacienteActividadFisicas { get; set; }
    }
}
