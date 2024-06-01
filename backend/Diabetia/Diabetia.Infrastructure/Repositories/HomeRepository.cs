using Diabetia.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Diabetia.Common.Utilities;
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


        public async Task<int> GetPhysicalActivity(string Email, int IdEvent)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            // Buscar todos los registros en CargaEvento que coincidan con el IdUsuario y el IdEvento
            int TotalPhysicalActivity = (int)(from eaf in _context.EventoActividadFisicas
                                         join ce in _context.CargaEventos
                                         on eaf.IdCargaEvento equals ce.Id
                                         where ce.IdPaciente == patient.Id && ce.IdTipoEvento == IdEvent
                                         select eaf.Duracion)
                                         .Sum();

            return TotalPhysicalActivity;

         }
        

        public async Task<int> GetChMetrics(string Email, int IdEvent)
        {
            var user =  _context.Usuarios.FirstOrDefault(u => u.Email == Email);
            var patient =  _context.Pacientes.FirstOrDefault(u => u.IdUsuario == user.Id);
            // Buscar todos los registros en CargaEvento que coincidan con el IdUsuario y el IdEvento
            int TotalCh = (int)(from ec in _context.EventoComida
                                              join ce in _context.CargaEventos
                                              on ec.IdCargaEvento equals ce.Id
                                              where ce.IdPaciente == patient.Id && ce.IdTipoEvento == IdEvent
                                select ec.Carbohidratos)
                                              .Sum();

            return TotalCh;
        }

        public async Task<int> GetGlucose(string Email, int IdEvent)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == Email);
            var patient = _context.Pacientes.FirstOrDefault(p => p.IdUsuario == user.Id);

            var LastRegister = await _context.EventoGlucosas.OrderByDescending(eg => eg.Id).FirstOrDefaultAsync();

            int LastGlucoseRegister = (int)LastRegister.Glucemia;


            return LastGlucoseRegister;
        }

        public async Task<int> GetHypoglycemia(string Email)
        {
            int hipo = (int)GlucoseEnum.HIPOGLUCEMIA;
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == Email);
            var patient = _context.Pacientes.FirstOrDefault(p => p.IdUsuario == user.Id);

            var Hipoglycemias = await _context.EventoGlucosas.Where(eg => eg.Glucemia < hipo)
                                                            .SumAsync(eg => eg.Glucemia);
            int TotalHipoglycemias = (int)Hipoglycemias; 

            return TotalHipoglycemias;
        }

        public async Task<int> GetHyperglycemia(string Email)
        {
            int hiper = (int)GlucoseEnum.HIPERGLUCEMIA;
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == Email);
            var patient = _context.Pacientes.FirstOrDefault(p => p.IdUsuario == user.Id);

            var Hiperglycemias = await _context.EventoGlucosas.Where(eg => eg.Glucemia  > hiper)
                                                            .SumAsync(eg => eg.Glucemia);

            int TotalHiperglycemias = (int)Hiperglycemias;

            return TotalHiperglycemias;
        }

        public async Task<int> GetInsulin(string Email, int IdEvent)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == Email);
            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);

            var InsulinPerDay = (from ei in _context.EventoInsulinas
                                 join ce in _context.CargaEventos
                                 on ei.IdCargaEvento equals ce.Id
                                 where ce.IdPaciente == patient.Id && ce.IdTipoEvento == IdEvent
                                 select ei.InsulinaInyectada)
                                         .Sum();

            int UnitsOfInsulinPerDay = (int)InsulinPerDay;

            return UnitsOfInsulinPerDay;
        }
    }
        
}
