using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            Profesionals = new HashSet<Profesional>();
        }

        public int Id { get; set; }
        public string? NombreEspecialidad { get; set; }

        public virtual ICollection<Profesional> Profesionals { get; set; }
    }
}
