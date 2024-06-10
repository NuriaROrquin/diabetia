
namespace Diabetia.Domain.Models
{
    public partial class UnidadMedidaIngrediente
    {
        public UnidadMedidaIngrediente()
        {
            Ingredientes = new HashSet<Ingrediente>();
        }

        public int Id { get; set; }
        public string Medida { get; set; } = null!;
        public string Abreviacion { get; set; } = null!;

        public virtual ICollection<Ingrediente> Ingredientes { get; set; }
    }
}
