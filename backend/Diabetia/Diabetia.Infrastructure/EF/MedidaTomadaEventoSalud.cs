using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class MedidaTomadaEventoSalud
    {
        public int Id { get; set; }
        public int IdEventoSalud { get; set; }
        public int IdMedidaTomada { get; set; }

        public virtual EventoSalud IdEventoSaludNavigation { get; set; } = null!;
        public virtual MedidaTomadaSintoma IdMedidaTomadaNavigation { get; set; } = null!;
    }
}
