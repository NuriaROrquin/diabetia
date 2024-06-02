using Diabetia.Infrastructure.EF;
using Diabetia.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Models;
using Diabetia.Domain.Entities;

namespace Diabetia.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private diabetiaContext _context;

        public UserRepository(diabetiaContext context)
        {
            _context = context;
        }

        public async Task CompleteUserInfo(string name, string email, string gender, string lastname, int weight, string phone, DateOnly birthdate)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            var pac = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);

            if (pac == null)
            {
                var pac_new = new Paciente
                {
                    IdUsuario = user.Id,
                    Peso = weight,
                    IdTipoDiabetes = 0
                };
                _context.Pacientes.Add(pac_new);
            }

            if (user != null)
            {
                user.NombreCompleto = String.Concat(name, " ", lastname);
                user.Genero = gender;
                user.Telefono = phone;
                user.FechaNacimiento = birthdate;
                _context.Usuarios.Update(user);
            }
           
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserInfo(int typeDiabetes, bool useInsuline, int typeInsuline, string email, bool needsReminder, int frequency, string hourReminder)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            var pac = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            var insulina_pac = await _context.InsulinaPacientes.FirstOrDefaultAsync(u => u.IdPaciente == pac.Id);


            pac.IdTipoDiabetes = typeDiabetes;
            pac.UsaInsulina = useInsuline;
            pac.IdSensibilidadInsulina = 1;

            _context.Pacientes.Update(pac);

            if (insulina_pac == null)
            {
                if (useInsuline == true)
                {
                    var insulina_pac_new = new InsulinaPaciente
                    {

                        IdPaciente = pac.Id,
                        IdTipoInsulina = typeInsuline,
                        Frecuencia = frequency
                    };

                    _context.InsulinaPacientes.Add(insulina_pac_new);

                }

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

        public async Task CompleteDeviceslUserInfo(string email, bool tieneDispositivo, int? idDispositivo, int? frecuencia)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            var pac = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            var pac_div = await _context.DispositivoPacientes.FirstOrDefaultAsync(u => u.IdPaciente == pac.Id);

            if (pac_div == null)
            {
                if (tieneDispositivo == true)
                {
                    var pac_new = new DispositivoPaciente
                    {
                        IdPaciente = pac.Id,
                        IdDispositivo = idDispositivo,
                        Frecuencia = frecuencia
                    };
                    _context.DispositivoPacientes.Add(pac_div);
                }
            }
            else
            {
                pac_div.IdPaciente = pac.Id;
                pac_div.IdDispositivo = idDispositivo;
                pac_div.Frecuencia = frecuencia;
                _context.DispositivoPacientes.Update(pac_div);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<Usuario> GetUserInfo(string userName)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == userName);

            return user;
        }
    }
}

