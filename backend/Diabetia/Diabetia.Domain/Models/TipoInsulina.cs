using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class TipoInsulina
    {
        public TipoInsulina()
        {
            InsulinaPacientes = new HashSet<InsulinaPaciente>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdTipoAccionInsulina { get; set; }
        public int Duracion { get; set; }

        public virtual TipoAccionInsulina IdTipoAccionInsulinaNavigation { get; set; } = null!;
        public virtual ICollection<InsulinaPaciente> InsulinaPacientes { get; set; }
    }
}
