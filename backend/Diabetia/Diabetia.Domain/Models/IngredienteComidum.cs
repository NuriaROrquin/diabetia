
namespace Diabetia.Domain.Models
{
    public partial class IngredienteComidum
    {
        public int Id { get; set; }
        public int IdIngrediente { get; set; }
        public int IdEventoComida { get; set; }
        public int CantidadIngerida { get; set; }
        public decimal? Proteinas { get; set; }
        public decimal? GrasasTotales { get; set; }
        public decimal? Carbohidratos { get; set; }
        public decimal? Sodio { get; set; }
        public decimal? FibraAlimentaria { get; set; }

        public virtual EventoComidum IdEventoComidaNavigation { get; set; } = null!;
        public virtual Ingrediente IdIngredienteNavigation { get; set; } = null!;
    }
}
