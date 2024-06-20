using Diabetia.Domain.Models;
using Diabetia.Domain.Utilities.Validations;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;

namespace Diabetia.API.DTO.DataUserRequest
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

        public Paciente ToDomain()
        {
            var patient = new Paciente();

            patient.IdUsuarioNavigation = new Usuario()
            {
                NombreCompleto = String.Concat(Name, " ", Lastname),
                FechaNacimiento = Birthdate,
                Genero = Gender,
                Telefono = Phone
            };

            patient.Peso = Weight;
            return patient;
        }
    }

    public class PatientRequest
    {
        public string Email { get; set; }
        public int TypeDiabetes { get; set; }
        public bool UseInsuline { get; set; }
        public int? TypeInsuline { get; set; }
        public int? Frequency { get; set; }
        public bool? NeedsReminder { get; set; }
        public string? HourReminder { get; set; }
        public int? InsulinePerCH { get; set; }

        public Paciente ToDomain()
        {
            var patient = new Paciente();


            patient.UsaInsulina = UseInsuline;
            patient.IdTipoDiabetes = TypeDiabetes;
            patient.IdSensibilidadInsulina = 1;
            patient.CorreccionCh = InsulinePerCH;

            return patient;
        }
    }

    public class PhysicalRequest
    {
        public string Email { get; set; }
        public int IdActividadFisica { get; set; }
        public int Frecuencia { get; set; }
        public int Duracion { get; set; }
        public bool HaceActividadFisica { get; set; }

        public PacienteActividadFisica ToDomain()
        {
            var patient_actfisica = new PacienteActividadFisica();

            patient_actfisica.IdActividadFisica = IdActividadFisica;
            patient_actfisica.Frecuencia = Frecuencia;
            patient_actfisica.Duracion = Duracion;
           
            return patient_actfisica;
        }

    }

}
