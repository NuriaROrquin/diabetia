using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            CargaEventos = new HashSet<CargaEvento>();
            DispositivoPacientes = new HashSet<DispositivoPaciente>();
            EncargadoLegals = new HashSet<EncargadoLegal>();
            InsulinaPacientes = new HashSet<InsulinaPaciente>();
            PacienteActividadFisicas = new HashSet<PacienteActividadFisica>();
            PacienteEnfermedadPreexistentes = new HashSet<PacienteEnfermedadPreexistente>();
            VinculoProfesionalPacientes = new HashSet<VinculoProfesionalPaciente>();
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int? IdTipoDiabetes { get; set; }
        public int? Peso { get; set; }
        public int? Altura { get; set; }
        public bool? UsaInsulina { get; set; }
        public DateOnly? FechaDiagnostico { get; set; }
        public int? MinGlucosa { get; set; }
        public int? MaxGlucosa { get; set; }
        public int? CorreccionCh { get; set; }
        public int IdSensibilidadInsulina { get; set; }

        public virtual SensibilidadInsulina IdSensibilidadInsulinaNavigation { get; set; } = null!;
        public virtual TipoDiabete? IdTipoDiabetesNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<CargaEvento> CargaEventos { get; set; }
        public virtual ICollection<DispositivoPaciente> DispositivoPacientes { get; set; }
        public virtual ICollection<EncargadoLegal> EncargadoLegals { get; set; }
        public virtual ICollection<InsulinaPaciente> InsulinaPacientes { get; set; }
        public virtual ICollection<PacienteActividadFisica> PacienteActividadFisicas { get; set; }
        public virtual ICollection<PacienteEnfermedadPreexistente> PacienteEnfermedadPreexistentes { get; set; }
        public virtual ICollection<VinculoProfesionalPaciente> VinculoProfesionalPacientes { get; set; }
    }
}
