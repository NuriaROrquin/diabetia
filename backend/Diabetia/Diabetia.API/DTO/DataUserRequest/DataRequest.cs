using Diabetia.Domain.Models;

namespace Diabetia.API.DTO.DataUserRequest
{
    public class DataRequest2
    {
        public string Name { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int Weight { get; set; }
        public string Lastname { get; set; }

        public Usuario ToDomain(DataRequest request)
        {
            var usuario = new Usuario();
            usuario.NombreCompleto = String.Concat(request.Name, " ", request.Lastname);
            usuario.FechaNacimiento = request.Birthdate;
            usuario.Genero = request.Gender;
            usuario.Telefono = request.Phone;
        }
    }





}
