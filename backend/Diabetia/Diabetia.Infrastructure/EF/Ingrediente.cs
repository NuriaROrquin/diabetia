using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class Ingrediente
    {
        public Ingrediente()
        {
            IngredienteComida = new HashSet<IngredienteComidum>();
        }

        public int Id { get; set; }
        public int IdUnidadMedidaIngrediente { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal PorcionEstandar { get; set; }
        public decimal Proteinas { get; set; }
        public decimal Carbohidratos { get; set; }
        public decimal GrasasTotales { get; set; }
        public decimal? FibraAlimentaria { get; set; }
        public decimal? Sodio { get; set; }
        public int? IdUsuario { get; set; }
        public bool? FueChequeado { get; set; }
        public decimal? Kilocalorias { get; set; }

        public virtual UnidadMedidaIngrediente IdUnidadMedidaIngredienteNavigation { get; set; } = null!;
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<IngredienteComidum> IngredienteComida { get; set; }
    }
}
