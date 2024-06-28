namespace Diabetia.Domain.Models
{
    public partial class EventoComidum
    {
        public EventoComidum()
        {
            EventoInsulinas = new HashSet<EventoInsulina>();
            IngredienteComida = new HashSet<IngredienteComidum>();
        }

        public int Id { get; set; }
        public int IdCargaEvento { get; set; }
        public int IdTipoCargaComida { get; set; }
        public decimal Carbohidratos { get; set; }
        public decimal? Proteinas { get; set; }
        public decimal? GrasasTotales { get; set; }
        public decimal? FibraAlimentaria { get; set; }
        public decimal? Sodio { get; set; }
        public string? Imagen { get; set; }
        public bool? FueHipo { get; set; }

        public virtual CargaEvento IdCargaEventoNavigation { get; set; } = null!;
        public virtual TipoCargaComidum IdTipoCargaComidaNavigation { get; set; } = null!;
        public virtual ICollection<EventoInsulina> EventoInsulinas { get; set; }
        public virtual ICollection<IngredienteComidum> IngredienteComida { get; set; }
    }
}
