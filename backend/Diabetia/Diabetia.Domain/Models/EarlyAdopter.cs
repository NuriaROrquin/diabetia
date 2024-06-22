using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class EarlyAdopter
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public sbyte? Visto { get; set; }
    }
}
