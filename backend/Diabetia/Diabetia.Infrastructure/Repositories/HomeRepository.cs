using Diabetia.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Diabetia.Domain.Utilities;
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

        public async Task<int?> GetPhysicalActivity(string email, int idEvent, DateFilter? dateFilter)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            var patient = await _context.Pacientes.FirstOrDefaultAsync(u => u.IdUsuario == user.Id);

            var query = _context.EventoActividadFisicas
                .Join(_context.CargaEventos,
                        eaf => eaf.IdCargaEvento,
                        ce => ce.Id,
                        (eaf, ce) => new { eaf, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id && joined.ce.IdTipoEvento == idEvent && joined.ce.FueRealizado == true);

            if (dateFilter != null)
            {
                if (dateFilter.DateFrom.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento >= dateFilter.DateFrom);
                }

                if (dateFilter.DateTo.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento <= dateFilter.DateTo);
                }
            }

            var totalPhysicalActivity = await query.SumAsync(joined => joined.eaf.Duracion);

            return totalPhysicalActivity;
        }

        public async Task<decimal?> GetChMetrics(string email, int idEvent, DateFilter? dateFilter)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return 0;

            var query = _context.EventoComida
                .Join(_context.CargaEventos,
                      ec => ec.IdCargaEvento,
                      ce => ce.Id,
                      (ec, ce) => new { ec, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id && joined.ce.IdTipoEvento == idEvent && joined.ce.FueRealizado == true);

            if (dateFilter != null)
            {
                if (dateFilter.DateFrom.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento >= dateFilter.DateFrom);
                }

                if (dateFilter.DateTo.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento <= dateFilter.DateTo);
                }
            }

            var totalCh = await query.SumAsync(joined => joined.ec.Carbohidratos);

            return totalCh;
        }

        public async Task<int> GetGlucose(string email, int IdEvent, DateFilter? dateFilter)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return 0;

            var query = _context.EventoGlucosas
                .Join(_context.CargaEventos,
                      eg => eg.IdCargaEvento,
                      ce => ce.Id,
                      (eg, ce) => new { eg, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id && joined.ce.IdTipoEvento == IdEvent && joined.ce.FueRealizado == true);

            if (dateFilter != null)
            {
                if (dateFilter.DateFrom.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento >= dateFilter.DateFrom);
                }

                if (dateFilter.DateTo.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento <= dateFilter.DateTo);
                }
            }

            var lastRegister = await query
                .OrderByDescending(joined => joined.ce.Id)
                .Select(joined => joined.eg.Glucemia)
                .FirstOrDefaultAsync();

            if (lastRegister == null) return 0;

            int lastGlucoseRegister = (int)lastRegister;

            return lastGlucoseRegister;
        }

        public async Task<int> GetHypoglycemia(string email, DateFilter? dateFilter)
        {
            int hipo = (int)GlucoseEnum.HIPOGLUCEMIA;

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);

            var query = _context.EventoGlucosas
                .Join(_context.CargaEventos,
                      eg => eg.IdCargaEvento,
                      ce => ce.Id,
                      (eg, ce) => new { eg, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id && joined.ce.FueRealizado == true && joined.eg.Glucemia < hipo);

            if (dateFilter != null)
            {
                if (dateFilter.DateFrom.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento >= dateFilter.DateFrom);
                }

                if (dateFilter.DateTo.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento <= dateFilter.DateTo);
                }
            }

            var totalHipoglycemias = await query.CountAsync();

            return totalHipoglycemias;
        }

        public async Task<int> GetHyperglycemia(string email, DateFilter? dateFilter)
        {
            int hiper = (int)GlucoseEnum.HIPERGLUCEMIA;

            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return 0;

            var query = _context.EventoGlucosas
                .Join(_context.CargaEventos,
                      eg => eg.IdCargaEvento,
                      ce => ce.Id,
                      (eg, ce) => new { eg, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id && joined.ce.FueRealizado == true && joined.eg.Glucemia > hiper);

            if (dateFilter != null)
            {
                if (dateFilter.DateFrom.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento >= dateFilter.DateFrom);
                }

                if (dateFilter.DateTo.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento <= dateFilter.DateTo);
                }
            }

            var totalHiperglycemias = await query.CountAsync();

            return totalHiperglycemias;
        }

        public async Task<int?> GetInsulin(string email, int idEvent, DateFilter? dateFilter)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return 0;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return 0;

            var query = _context.EventoInsulinas
                .Join(_context.CargaEventos,
                      ei => ei.IdCargaEvento,
                      ce => ce.Id,
                      (ei, ce) => new { ei, ce })
                .Where(joined => joined.ce.IdPaciente == patient.Id && joined.ce.IdTipoEvento == idEvent && joined.ce.FueRealizado == true);

            if (dateFilter != null)
            {
                if (dateFilter.DateFrom.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento >= dateFilter.DateFrom);
                }

                if (dateFilter.DateTo.HasValue)
                {
                    query = query.Where(joined => joined.ce.FechaEvento <= dateFilter.DateTo);
                }
            }

            var unitsOfInsulinPerDay = await query.SumAsync(joined => joined.ei.InsulinaInyectada);

            return unitsOfInsulinPerDay;
        }

        public async Task<List<CargaEvento>> GetLastEvents(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuario == user.Id);
            if (patient == null) return null;

            var query = _context.CargaEventos
                .Where(ce => ce.IdPaciente == patient.Id && ce.FueRealizado == true);

            var lastEvents = await query
                .OrderByDescending(ce => ce.Id)
                .Take(10)
                .ToListAsync();

            return lastEvents;
        }

    }

}
