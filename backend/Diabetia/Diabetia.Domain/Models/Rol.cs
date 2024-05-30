using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Model
{
    public partial class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string? Rol1 { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
