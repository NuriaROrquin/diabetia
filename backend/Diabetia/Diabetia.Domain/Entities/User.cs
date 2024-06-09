
namespace Diabetia.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public bool InitialFormCompleted { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public double? Weight { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string Username { get; set; }
    }

    public class Patient : User
    {
        public int? TypeDiabetes { get; set; }
        public bool? UseInsuline { get; set; }
        public int? TypeInsuline { get; set; }
        public int? Frequency { get; set; }

    }

    public class Exercise_Patient : User
    {
        public int? IdActividadFisica { get; set; }
        public int? Frecuencia { get; set; }
        public int? Duracion { get; set; }

    }

    public class Device_Patient : User
    {
        public int? IdDispositivo { get; set; }
        public int? Frecuencia { get; set; }

    }
}
