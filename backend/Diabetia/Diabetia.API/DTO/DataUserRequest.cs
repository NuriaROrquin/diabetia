namespace Diabetia.API
{
    public class DataRequest
    {
        public string Name { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int Weight { get; set; }
        public string Lastname { get; set; }
    }


    public class PatientRequest
    {
        public string Email { get; set; }
        public int TypeDiabetes { get; set; }
        public bool UseInsuline { get; set; }
        public int TypeInsuline { get; set; }
        public int Frequency { get; set; }
        public bool NeedsReminder { get; set; }
        public string HourReminder { get; set; }
    }

    public class PhysicalRequest
    {
        public string Email { get; set; }
        public int IdActividadFisica { get; set; }
        public int Frecuencia { get; set; }
        public int Duracion { get; set; }
        public bool HaceActividadFisica { get; set; }

    }

    public class IllnessRequest
    {
        public string Email { get; set; }
        public int IdEnfermedad { get; set; }

    }

    public class DevicesRequest
    {
        public string Email { get; set; }
        public bool TieneDispositivo { get; set; }
        public int? IdDispositivo { get; set; }
        public int? Frecuencia { get; set; }

    }
}
