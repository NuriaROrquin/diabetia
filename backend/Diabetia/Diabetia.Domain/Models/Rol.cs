

namespace Diabetia.Domain.Models
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
