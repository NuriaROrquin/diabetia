using Diabetia.Domain.Models;

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
            var usuario = new Usuario();
            var patient = new Paciente();

            usuario.NombreCompleto = String.Concat(Name, " ", Lastname);
            usuario.FechaNacimiento = Birthdate;
            usuario.Genero = Gender;
            usuario.Telefono = Phone;

            patient.Peso = Weight;
            return patient;
        }
    }





}
