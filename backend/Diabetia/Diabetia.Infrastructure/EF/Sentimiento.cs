using System;
using System.Collections.Generic;

namespace Diabetia.API
{
    public partial class Sentimiento
    {
        public Sentimiento()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Sentimiento1 { get; set; } = null!;
        public string? Emoji { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
