

namespace Diabetia.Domain.Models
{
    public partial class DiaSemana
    {
        public DiaSemana()
        {
            RecordatorioEventos = new HashSet<RecordatorioEvento>();
        }

        public int Id { get; set; }
        public string Dia { get; set; } = null!;

        public virtual ICollection<RecordatorioEvento> RecordatorioEventos { get; set; }
    }
}
