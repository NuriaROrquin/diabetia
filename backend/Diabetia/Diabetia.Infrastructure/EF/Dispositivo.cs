using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class Dispositivo
    {
        public Dispositivo()
        {
            DispositivoPacientes = new HashSet<DispositivoPaciente>();
        }

        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Marca { get; set; } = null!;

        public virtual ICollection<DispositivoPaciente> DispositivoPacientes { get; set; }
    }
}
