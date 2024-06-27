

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
    }
}
