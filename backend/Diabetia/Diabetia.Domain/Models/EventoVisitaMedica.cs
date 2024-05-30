using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Model
{
    public partial class EventoVisitaMedica
    {
        public int Id { get; set; }
        public int? IdProfesional { get; set; }
        public int IdCargaEvento { get; set; }
        public string? Descripcion { get; set; }

        public virtual CargaEvento IdCargaEventoNavigation { get; set; } = null!;
        public virtual Profesional? IdProfesionalNavigation { get; set; }
    }
}
