using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class CargaEvento
    {
        public CargaEvento()
        {
            EventoActividadFisicas = new HashSet<EventoActividadFisica>();
            EventoComida = new HashSet<EventoComidum>();
            EventoEstudios = new HashSet<EventoEstudio>();
            EventoGlucosas = new HashSet<EventoGlucosa>();
            EventoInsulinas = new HashSet<EventoInsulina>();
            EventoSaluds = new HashSet<EventoSalud>();
            EventoVisitaMedicas = new HashSet<EventoVisitaMedica>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdTipoEvento { get; set; }
        public DateTime? FechaActual { get; set; }
        public DateTime FechaEvento { get; set; }
        public string? NotaLibre { get; set; }
        public bool? FueRealizado { get; set; }
        public bool? EsNotaLibre { get; set; }

        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
        public virtual TipoEvento IdTipoEventoNavigation { get; set; } = null!;
        public virtual ICollection<EventoActividadFisica> EventoActividadFisicas { get; set; }
        public virtual ICollection<EventoComidum> EventoComida { get; set; }
        public virtual ICollection<EventoEstudio> EventoEstudios { get; set; }
        public virtual ICollection<EventoGlucosa> EventoGlucosas { get; set; }
        public virtual ICollection<EventoInsulina> EventoInsulinas { get; set; }
        public virtual ICollection<EventoSalud> EventoSaluds { get; set; }
        public virtual ICollection<EventoVisitaMedica> EventoVisitaMedicas { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
