
namespace Diabetia.Domain.Models
{
    public partial class TipoDiabete
    {
        public TipoDiabete()
        {
            Pacientes = new HashSet<Paciente>();
        }

        public int Id { get; set; }
        public string? Tipo { get; set; }

        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
