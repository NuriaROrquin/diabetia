using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Model
{
    public partial class EventoEstudio
    {
        public int Id { get; set; }
        public int IdProfesional { get; set; }
        public int IdCargaEvento { get; set; }
        public string? Archivo { get; set; }
        public string? TipoEstudio { get; set; }

        public virtual CargaEvento IdCargaEventoNavigation { get; set; } = null!;
        public virtual Profesional IdProfesionalNavigation { get; set; } = null!;
    }
}
