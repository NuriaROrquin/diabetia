using Diabetia.Infrastructure.EF;
using Diabetia.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Models;

namespace Diabetia.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private diabetiaContext _context;

        public UserRepository(diabetiaContext context)
        {
            _context = context;
        }

        public async Task CompleteUserInfo(string name, string email, string gender, string lastname, int weight, string phone)
        {

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                user.NombreCompleto = String.Concat(name, " ", lastname);
                user.Genero = gender;
                user.Telefono = phone;
            }
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserInfo(int typeDiabetes, bool useInsuline, string typeInsuline, string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            var pac = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);

            if (pac == null)
            {
                var pac_new = new Paciente
                {
                    IdUsuario = user.Id,
                    IdTipoDiabetes = typeDiabetes,
                    UsaInsulina = useInsuline,
                    IdSensibilidadInsulina = 1,
                };
                _context.Pacientes.Add(pac_new);
            }
            else
            {
                pac.IdUsuario = user.Id;
                pac.IdTipoDiabetes = typeDiabetes;
                pac.UsaInsulina = useInsuline;
                pac.IdSensibilidadInsulina = 1;
                _context.Pacientes.Update(pac);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> GetInformationCompleted(string username)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
            var pac = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);

            if (pac == null)
            {
                return false;
            }

            bool allFieldsNotNull = user.NombreCompleto != null &&
                            user.Genero != null &&
                            user.Telefono != null &&
                            pac?.IdTipoDiabetes != null &&
                            pac?.UsaInsulina != null &&
                            pac?.IdSensibilidadInsulina != null;

            return allFieldsNotNull;
        }

        public async Task CompletePhysicalUserInfo(string email, bool haceActividadFisica, int frecuencia, int idActividadFisica, int duracion)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            var pac = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            var pac_phy = await _context.PacienteActividadFisicas.FirstOrDefaultAsync(u => u.IdPaciente == pac.Id);

            if (pac_phy == null)
            {
                var pac_new = new PacienteActividadFisica
                {
                    IdPaciente = pac.Id,
                    IdActividadFisica = idActividadFisica,
                    Frecuencia = frecuencia,
                    Duracion = duracion
                };
                _context.PacienteActividadFisicas.Add(pac_new);
            }
            else
            {
                pac_phy.IdPaciente = pac.Id;
                pac_phy.IdActividadFisica = idActividadFisica;
                pac_phy.Frecuencia = frecuencia;
                pac_phy.Duracion = duracion;
                _context.PacienteActividadFisicas.Update(pac_phy);
            }
            await _context.SaveChangesAsync();
        }
    }
    }
}
