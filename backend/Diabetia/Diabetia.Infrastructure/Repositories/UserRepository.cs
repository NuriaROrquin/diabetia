using Diabetia.API;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            else {
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
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreCompleto == username);
            var pac = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);

            bool allFieldsNotNull = user.NombreCompleto != null &&
                            user.Genero != null &&
                            user.Telefono != null &&
                            pac?.IdTipoDiabetes != null &&
                            pac?.UsaInsulina != null &&
                            pac?.IdSensibilidadInsulina != null;

            return allFieldsNotNull;
        }

        public async Task<IEnumerable<Event>> GetAllEvents(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);

            var events = await _context.CargaEventos
                .Where(ce => ce.IdPaciente == patient.Id)
                .Select(ce => new Event { TipoEvento = ce.IdTipoEvento, FechaEvento = ce.FechaEvento })
                .ToListAsync();

            return events;
        }
    }
}
