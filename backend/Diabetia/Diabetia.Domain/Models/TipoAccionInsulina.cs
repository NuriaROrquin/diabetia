using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Model
{
    public partial class TipoAccionInsulina
    {
        public TipoAccionInsulina()
        {
            TipoInsulinas = new HashSet<TipoInsulina>();
        }

        public int Id { get; set; }
        public string TipoAccion { get; set; } = null!;

        public virtual ICollection<TipoInsulina> TipoInsulinas { get; set; }
    }
}
