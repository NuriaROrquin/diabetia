using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class EncargadoLegal
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdUsuario { get; set; }
        public string? RelacionConPaciente { get; set; }
        public bool PuedeEditarDatos { get; set; }

        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
