using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Utilities;
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
                    joined.CargaEvento.FechaEvento.Date >= dateFrom &&
                    joined.CargaEvento.FechaEvento.Date <= dateTo &&
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

        public async Task<List<EventSummary>> GetInsulinEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo)
        {
            var results = await _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId && ce.FechaEvento.Date >= dateFrom && ce.FechaEvento.Date <= dateTo)
                .Join(
                    _context.EventoInsulinas,
                    ce => ce.Id,
                    ei => ei.IdCargaEvento,
                    (ce, ei) => new { CargaEvento = ce, EventoInsulina = ei }
                )
                .GroupBy(joined => joined.CargaEvento.FechaEvento.Date)
                .Select(g => new EventSummary
                {
                    EventDay = g.Key,
                    AmountEvents = g.Count()
                })
                .OrderBy(result => result.EventDay)
                .ToListAsync();

            return results;
        }

        // -------------------------------------------------------- ⬇⬇ Physical Activity Report ⬇⬇ -------------------------------------------------------
        public async Task<List<EventSummary>> GetPhysicalActivityEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo)
        {
            var results = await _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId && ce.FechaEvento.Date >= dateFrom && ce.FechaEvento.Date <= dateTo)
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
                .Select(g => new EventSummary
                {
                    EventDay = g.Key,
                    AmountEvents = g.Sum(x => x.EventCount)
                })
                .OrderBy(result => result.EventDay)
                .ToListAsync();

            return results;
        }

        public async Task<List<ActivityDurationSummary>> GetPhysicalActivityEventDurationsByPatientId(int patientId, DateTime dateFrom, DateTime dateTo)
        {
            var results = await _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId && ce.FechaEvento.Date >= dateFrom && ce.FechaEvento.Date <= dateTo)
                .Join(
                    _context.EventoActividadFisicas,
                    ce => ce.Id,
                    eaf => eaf.IdCargaEvento,
                    (ce, eaf) => new { ce, eaf }
                )
                .Join(
                    _context.ActividadFisicas,
                    combined => combined.eaf.IdActividadFisica,
                    af => af.Id,
                    (combined, af) => new { combined.ce, combined.eaf, af }
                )
                .GroupBy(x => x.af.Nombre)
                .Select(g => new ActivityDurationSummary
                {
                    ActivityName = g.Key,
                    TotalDuration = g.Sum(x => x.eaf.Duracion)
                })
                .OrderBy(result => result.ActivityName)
                .ToListAsync();

            return results;
        }

        // -------------------------------------------------------- ⬇⬇ Glucose Report ⬇⬇ -------------------------------------------------------
        public async Task<List<EventSummary>> GetGlucoseEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo)
        {
            var results = await _context.CargaEventos
                   .Where(ce => ce.IdPaciente == patientId && ce.FechaEvento.Date >= dateFrom && ce.FechaEvento.Date <= dateTo)
                   .Join(
                       _context.EventoGlucosas,
                       ce => ce.Id,
                       eg => eg.IdCargaEvento,
                       (ce, eg) => new { CargaEvento = ce, EventoGlucosa = eg }
                   )
                   .GroupBy(joined => joined.CargaEvento.FechaEvento.Date)
                   .Select(g => new EventSummary
                   {
                       EventDay = g.Key,
                       AmountEvents = g.Count()
                   })
                   .OrderBy(result => result.EventDay)
                   .ToListAsync();

            return results;
        }

        public async Task<List<GlucoseMeasurement>> GetHyperglycemiaGlucoseHistoryByPatientId(int patientId, GlucoseEnum hyperglycemiaValue)
        {
            var results = await _context.CargaEventos
            .Where(ce => ce.IdPaciente == patientId)
            .Join(
                _context.EventoGlucosas,
                ce => ce.Id,
                eg => eg.IdCargaEvento,
                (ce, eg) => new { CargaEvento = ce, EventoGlucosa = eg }
            )
            .Where(joined => joined.EventoGlucosa.Glucemia >= (int)hyperglycemiaValue)
            .OrderByDescending(joined => joined.CargaEvento.FechaEvento)
            .Select(joined => new GlucoseMeasurement
            {
                MeasurementDate = joined.CargaEvento.FechaEvento,
                GlucoseLevel = (int)joined.EventoGlucosa.Glucemia
            })
            .ToListAsync();

                return results;
        }

        public async Task<List<GlucoseMeasurement>> GetHypoglycemiaGlucoseHistoryByPatientId(int patientId, GlucoseEnum hypoglycemiaValue)
        {
            var results = await _context.CargaEventos
            .Where(ce => ce.IdPaciente == patientId)
            .Join(
                _context.EventoGlucosas,
                ce => ce.Id,
                eg => eg.IdCargaEvento,
                (ce, eg) => new { CargaEvento = ce, EventoGlucosa = eg }
            )
            .Where(joined => joined.EventoGlucosa.Glucemia <= (int)hypoglycemiaValue)
            .OrderByDescending(joined => joined.CargaEvento.FechaEvento)
            .Select(joined => new GlucoseMeasurement
            {
                MeasurementDate = joined.CargaEvento.FechaEvento,
                GlucoseLevel = (int)joined.EventoGlucosa.Glucemia
            })
            .ToListAsync();

            return results;
        }

        public async Task<List<GlucoseMeasurement>> GetGlucoseEventsToReportByPatientId(int patientId, DateTime dateFrom, DateTime dateTo)
        {
            var results = await _context.CargaEventos
        .Where(ce => ce.IdPaciente == patientId && ce.FechaEvento.Date >= dateFrom && ce.FechaEvento.Date <= dateTo)
        .Join(
            _context.EventoGlucosas,
            ce => ce.Id,
            eg => eg.IdCargaEvento,
            (ce, eg) => new GlucoseMeasurement
            {
                MeasurementDate = ce.FechaEvento.Date,
                GlucoseLevel = eg.Glucemia
            }
        )
        .OrderBy(gm => gm.MeasurementDate)
        .ToListAsync();

            return results;
        }

        // -------------------------------------------------------- ⬇⬇ Food Report ⬇⬇ -------------------------------------------------------
        public async Task<List<EventSummary>> GetFoodEventSummaryByPatientId(int patientId, DateTime dateFrom, DateTime dateTo)
        {
            var results = await _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId && ce.FechaEvento.Date >= dateFrom && ce.FechaEvento.Date <= dateTo)
                .Join(
                    _context.EventoComida,
                    ce => ce.Id,
                    ec => ec.IdCargaEvento,
                    (ce, ec) => new { CargaEvento = ce, EventoComida = ec }
                )
                .GroupBy(joined => joined.CargaEvento.FechaEvento.Date)
                .Select(g => new EventSummary
                {
                    EventDay = g.Key,
                    AmountEvents = g.Count()
                })
                .OrderBy(result => result.EventDay)
                .ToListAsync();

            return results;
        }


    }

}
