using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Diabetia.Infrastructure.Repositories
{
    public class ReportingRepository : IReportingRepository
    {
        private diabetiaContext _context;
        public ReportingRepository(diabetiaContext context) 
        {
            _context = context;
        }

        // -------------------------------------------------------- ⬇⬇ Insuline Report ⬇⬇ -------------------------------------------------------
        public async Task<List<EventoInsulina>> GetInsulinEventsToReportByPatientId(int patientId, DateTime dateFrom, DateTime dateTo)
        {
            var insulinEventId = 1;

            var events = await _context.CargaEventos
                .Join(
                    _context.EventoInsulinas,
                    ce => ce.Id,
                    ei => ei.IdCargaEvento,
                    (ce, ei) => new { CargaEvento = ce, EventoInsulina = ei }
                )
                .Where(joined =>
                    joined.CargaEvento.FechaEvento >= dateFrom &&
                    joined.CargaEvento.FechaEvento <= dateTo &&
                    joined.CargaEvento.IdTipoEvento == insulinEventId &&
                    joined.CargaEvento.IdPaciente == patientId
                )
                .Select(joined => new EventoInsulina
                {
                    Id = joined.EventoInsulina.Id,
                    IdCargaEvento = joined.EventoInsulina.IdCargaEvento,
                    InsulinaInyectada = joined.EventoInsulina.InsulinaInyectada,
                    IdCargaEventoNavigation = new CargaEvento
                    {
                        Id = joined.CargaEvento.Id,
                        FechaEvento = joined.CargaEvento.FechaEvento,
                        IdTipoEvento = joined.CargaEvento.IdTipoEvento,
                        IdPaciente = joined.CargaEvento.IdPaciente,
                    }
                })
                .ToListAsync();

            var groupedEvents = events
                .GroupBy(e => e.IdCargaEventoNavigation.FechaEvento.Date)
                .SelectMany(group => group.ToList())
                .ToList();

            return groupedEvents;
        }

        // -------------------------------------------------------- ⬇⬇ Physical Activity Report ⬇⬇ -------------------------------------------------------
        public async Task<List<PhysicalActivitySummary>> GetAmountPhysicalEventsToReportByPatientId(int patientId, DateTime dateFrom, DateTime dateTo)
        {
            var results = await _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId && ce.FechaEvento >= dateFrom && ce.FechaEvento <= dateTo)
                .Join(
                    _context.EventoActividadFisicas,
                    ce => ce.Id,
                    eaf => eaf.IdCargaEvento,
                    (ce, eaf) => new { CargaEvento = ce, EventoActividadFisica = eaf }
                )
                .Join(
                    _context.ActividadFisicas,
                    joined => joined.EventoActividadFisica.IdActividadFisica,
                    af => af.Id,
                    (joined, af) => new { EventDate = joined.CargaEvento.FechaEvento.Date, EventCount = 1 }
                )
                .GroupBy(joined => joined.EventDate)
                .Select(g => new PhysicalActivitySummary
                {
                    EventDay = g.Key,
                    AmountEvents = g.Sum(x => x.EventCount)
                })
                .OrderBy(result => result.EventDay)
                .ToListAsync();

            return results;
        }
    }
}
