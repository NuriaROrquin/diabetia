using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class EventoGlucosa
    {
        public int Id { get; set; }
        public int IdCargaEvento { get; set; }
        public int? IdDispositivoPaciente { get; set; }
        public int? IdEventoComida { get; set; }
        public decimal Glucemia { get; set; }
        public bool? MedicionPostComida { get; set; }

        public virtual CargaEvento IdCargaEventoNavigation { get; set; } = null!;
        public virtual DispositivoPaciente? IdDispositivoPacienteNavigation { get; set; }
        public virtual EventoComidum? IdEventoComidaNavigation { get; set; }
    }
}
