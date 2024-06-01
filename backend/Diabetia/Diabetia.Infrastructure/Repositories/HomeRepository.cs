using Diabetia.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Repositories;
using Diabetia.Infrastructure.EF;

namespace Diabetia.Infrastructure.Repositories
{
    public class HomeRepository : IHomeRepository
    {

        private diabetiaContext _context;

        public HomeRepository(diabetiaContext context)
        {
            _context = context;
        }


        public async Task<int> GetPhysicalActivity(string Email, int idEvent)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            // Buscar todos los registros en CargaEvento que coincidan con el IdUsuario y el IdEvento
            int TotalPhysicalActivity = (int)(from eaf in _context.EventoActividadFisicas
                                         join ce in _context.CargaEventos
                                         on eaf.IdCargaEvento equals ce.Id
                                         where ce.IdPaciente == patient.Id && ce.Id == idEvent
                                         select eaf.Duracion)
                                         .Sum();

            return TotalPhysicalActivity;

         }
        

        public async Task<int> GetChMetrics(string Email, int idEvent)
        {
            var user =  _context.Usuarios.FirstOrDefault(u => u.Email == Email);
            var patient =  _context.Pacientes.FirstOrDefault(u => u.IdUsuario == user.Id);
            // Buscar todos los registros en CargaEvento que coincidan con el IdUsuario y el IdEvento
            int TotalCh = (int)(from ec in _context.EventoComida
                                              join ce in _context.CargaEventos
                                              on ec.IdCargaEvento equals ce.Id
                                              where ce.IdPaciente == patient.Id && ce.Id == idEvent
                                              select ec.Carbohidratos)
                                              .Sum();

            return TotalCh;
        }

        public async Task<int> GetGlucose(string Email, int idEvent)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == Email);
            var patient = _context.Pacientes.FirstOrDefault(p => p.IdUsuario == user.Id);

            var LastRegister = await _context.EventoGlucosas.OrderByDescending(eg => eg.Id).FirstOrDefaultAsync();

            int LastGlucoseRegister = (int)LastRegister.Glucemia;


            return LastGlucoseRegister;
        }

    }
        
}
