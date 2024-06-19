using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class InsulinaPaciente
    {
        public InsulinaPaciente()
        {
            EventoInsulinas = new HashSet<EventoInsulina>();
        }

        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdTipoInsulina { get; set; }
        public int Frecuencia { get; set; }

        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
        public virtual TipoInsulina IdTipoInsulinaNavigation { get; set; } = null!;
        public virtual ICollection<EventoInsulina> EventoInsulinas { get; set; }
    }
}
