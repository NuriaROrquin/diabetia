using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class Profesional
    {
        public Profesional()
        {
            EventoEstudios = new HashSet<EventoEstudio>();
            EventoVisitaMedicas = new HashSet<EventoVisitaMedica>();
            VinculoProfesionalPacientes = new HashSet<VinculoProfesionalPaciente>();
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdEspecialidad { get; set; }
        public string? Matricula { get; set; }

        public virtual Especialidad IdEspecialidadNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<EventoEstudio> EventoEstudios { get; set; }
        public virtual ICollection<EventoVisitaMedica> EventoVisitaMedicas { get; set; }
        public virtual ICollection<VinculoProfesionalPaciente> VinculoProfesionalPacientes { get; set; }
    }
}
