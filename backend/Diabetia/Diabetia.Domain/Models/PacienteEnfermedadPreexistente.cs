
namespace Diabetia.Domain.Models
{
    public partial class PacienteEnfermedadPreexistente
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdEnfermedad { get; set; }

        public virtual Enfermedad IdEnfermedadNavigation { get; set; } = null!;
        public virtual Paciente IdPacienteNavigation { get; set; } = null!;
    }
}
