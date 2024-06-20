using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class DispositivoPaciente
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int? IdDispositivo { get; set; }
        public int? Frecuencia { get; set; }

        public virtual Dispositivo? IdDispositivoNavigation { get; set; }
        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
    }
}
