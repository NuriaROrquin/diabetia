using System;
using System.Collections.Generic;

namespace Diabetia.Domain.Models
{
    public partial class Dispositivo
    {
        public Dispositivo()
        {
            DispositivoPacientes = new HashSet<DispositivoPaciente>();
            EventoGlucosas = new HashSet<EventoGlucosa>();
        }

        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Marca { get; set; } = null!;

        public virtual ICollection<DispositivoPaciente> DispositivoPacientes { get; set; }
        public virtual ICollection<EventoGlucosa> EventoGlucosas { get; set; }
    }
}
