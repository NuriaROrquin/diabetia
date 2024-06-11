using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class SensibilidadInsulina
    {
        public int Id { get; set; }
        public string Nivel { get; set; } = null!;
    }
}
