using Diabetia.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Diabetia.Common.Utilities;
using Diabetia.Domain.Repositories;
using Diabetia.Infrastructure.EF;
using Diabetia.Domain.Models;

namespace Diabetia.Infrastructure.Repositories
{
    public class HomeRepository : IHomeRepository
    {

        private diabetiaContext _context;

        public HomeRepository(diabetiaContext context)
        {
            _context = context;
        }


        public async Task<int?> GetPhysicalActivity(string email, int idEvent, int Timelapse)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);
            if (patient == null) return 0;

            var timeLimit = DateTime.UtcNow.AddHours(-Timelapse);

            var totalPhysicalActivity = await _context.EventoActividadFisicas
                    .Join(_context.CargaEventos,
                            eaf => eaf.IdCargaEvento,
                            ce => ce.Id,
                            (eaf, ce) => new { eaf, ce })
                    .Where(joined => joined.ce.IdPaciente == patient.Id 
                                    && joined.ce.IdTipoEvento == idEvent
                                    && joined.ce.FechaActual >= timeLimit)
                    .SumAsync(joined => joined.eaf.Duracion);

            return totalPhysicalActivity;
         }
        

        public async Task<decimal?> GetChMetrics(string email, int idEvent, int Timelapse)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return 0;

            var timeLimit = DateTime.UtcNow.AddHours(-Timelapse);

            var totalCh = await _context.EventoComida
                .Join(_context.CargaEventos,
                      ec => ec.IdCargaEvento,
                      ce => ce.Id,
                      (ec, ce) => new { ec, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id
                                 && joined.ce.IdTipoEvento == idEvent
                                 && joined.ce.FechaActual >= timeLimit)
                .Select(joined => joined.ec.Carbohidratos)
                .SumAsync();

            return totalCh;
        }

        public async Task<int> GetGlucose(string email, int IdEvent, int Timelapse)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return 0;

            var timeLimit = DateTime.UtcNow.AddHours(-Timelapse);

            var averageGlucose = await _context.EventoGlucosas
                .Join(_context.CargaEventos,
                    eg => eg.IdCargaEvento,
                    ce => ce.Id,
                    (eg, ce) => new { eg, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id 
                                && joined.ce.FechaActual >= timeLimit)
                .Select(joined => joined.eg.Glucemia)
                .DefaultIfEmpty()
                .AverageAsync();

            return (int)averageGlucose;
        }

        public async Task<int> GetHypoglycemia(string email, int Timelapse)
        {
            int hipo = (int)GlucoseEnum.HIPOGLUCEMIA;

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var timeLimit = DateTime.UtcNow.AddHours(-Timelapse);

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return 0;

            var totalHipoglycemias = await _context.EventoGlucosas
                .Join(_context.CargaEventos,
                      eg => eg.IdCargaEvento,
                      ce => ce.Id,
                      (eg, ce) => new { eg, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id && joined.eg.Glucemia < hipo && joined.ce.FechaActual >= timeLimit)
                .CountAsync();

            return totalHipoglycemias;
        }

        public async Task<int> GetHyperglycemia(string email, int Timelapse)
        {
            int hiper = (int)GlucoseEnum.HIPERGLUCEMIA;

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return 0;

            var timeLimit = DateTime.UtcNow.AddHours(-Timelapse);

            var totalHiperglycemias = await _context.EventoGlucosas
                .Join(_context.CargaEventos,
                      eg => eg.IdCargaEvento,
                      ce => ce.Id,
                      (eg, ce) => new { eg, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id && joined.eg.Glucemia > hiper && joined.ce.FechaActual >= timeLimit)
                .CountAsync();

            return totalHiperglycemias;
        }

        public async Task<int?> GetInsulin(string email, int idEvent, int Timelapse)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0; 

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return 0;

            var timeLimit = DateTime.UtcNow.AddHours(-Timelapse);

            var unitsOfInsulinPerDay = await _context.EventoInsulinas
                .Join(_context.CargaEventos,
                      ei => ei.IdCargaEvento,
                      ce => ce.Id,
                      (ei, ce) => new { ei, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id && joined.ce.IdTipoEvento == idEvent && joined.ce.FechaActual >= timeLimit)
                .SumAsync(joined => joined.ei.InsulinaInyectada);

            return unitsOfInsulinPerDay;
        }
    }
        
}
