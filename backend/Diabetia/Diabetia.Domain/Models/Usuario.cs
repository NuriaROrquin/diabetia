using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            EncargadoLegals = new HashSet<EncargadoLegal>();
            Ingredientes = new HashSet<Ingrediente>();
            Pacientes = new HashSet<Paciente>();
            Profesionals = new HashSet<Profesional>();
        }

        public int Id { get; set; }
        public int IdRol { get; set; }
        public string Email { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string? Genero { get; set; }
        public string? Dni { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Pais { get; set; }
        public bool? EstaActivo { get; set; }
        public string? Hash { get; set; }
        public string? Username { get; set; }
        public int? IntentosFallidos { get; set; }
        public int? StepCompleted { get; set; }

        public virtual Rol IdRolNavigation { get; set; } = null!;
        public virtual ICollection<EncargadoLegal> EncargadoLegals { get; set; }
        public virtual ICollection<Ingrediente> Ingredientes { get; set; }
        public virtual ICollection<Paciente> Pacientes { get; set; }
        public virtual ICollection<Profesional> Profesionals { get; set; }
    }
}
