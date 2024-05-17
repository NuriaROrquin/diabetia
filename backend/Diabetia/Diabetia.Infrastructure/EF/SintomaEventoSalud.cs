using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class SintomaEventoSalud
    {
        public int Id { get; set; }
        public int IdEventoSalud { get; set; }
        public int IdSintoma { get; set; }
        public int IdGravedadSintoma { get; set; }
        public string? Descripcion { get; set; }

        public virtual EventoSalud IdEventoSaludNavigation { get; set; } = null!;
        public virtual GravedadSintoma IdGravedadSintomaNavigation { get; set; } = null!;
        public virtual Sintoma IdSintomaNavigation { get; set; } = null!;
    }
}
