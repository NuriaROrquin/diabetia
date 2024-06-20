using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class EventoInsulina
    {
        public int Id { get; set; }
        public int IdCargaEvento { get; set; }
        public int? IdInsulinaPaciente { get; set; }
        public int? IdEventoComida { get; set; }
        public int? InsulinaRecomendada { get; set; }
        public int? InsulinaInyectada { get; set; }
        public bool? InsulinaPreComida { get; set; }

        public virtual CargaEvento IdCargaEventoNavigation { get; set; } = null!;
        public virtual EventoComidum? IdEventoComidaNavigation { get; set; }
        public virtual InsulinaPaciente? IdInsulinaPacienteNavigation { get; set; }
    }
}
