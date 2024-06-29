using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Entities.Feedback;
using Diabetia.Domain.Repositories;
using Diabetia.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetia.Infrastructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly diabetiaContext _context;

        public FeedbackRepository(diabetiaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los eventos sin feedback y los agrupa en una lista.
        /// </summary>
        /// <param name="patientId">Id del paciente por el que se va a filtrar los eventos.</param>
        /// <returns>Una lista de diccionarios con eventos</returns>
        public async Task<List<Dictionary<string, object>>> GetAllEventsWithoutFeedback(int patientId)
        {
            var events = new List<Dictionary<string, object>>();

            var foodEvents = await GetFoodWithoutFeedback(patientId);
            var physicalActivityEvents = await GetPhysicalActivityWithoutFeedback(patientId);

            foreach (var foodEvent in foodEvents)
            {
                var eventDict = new Dictionary<string, object>
                {
                    { "Title", "Comida" },
                    { "IdEvent", foodEvent.EventId },
                    { "KindEventId", foodEvent.KindEventId},
                    { "EventDate", foodEvent.EventDate },
                    { "Carbohydrates", foodEvent.Carbohydrates }
                };
                events.Add(eventDict);
            }

            foreach (var activityEvent in physicalActivityEvents)
            {
                var eventDict = new Dictionary<string, object>
                {
                    { "Title", "Actividad Física" },
                    { "IdEvent", activityEvent.EventId },
                    { "KindEventId", activityEvent.KindEventId},
                    { "EventDate", activityEvent.EventDate },
                    { "ActivityName", activityEvent.ActivityName }
                };
                events.Add(eventDict);
            }

            return events;
        }

        // -------------------------------------------------------- ⬇⬇ Food Feedback ⬇⬇ -------------------------------------------------------
        public async Task<List<FoodSummary>> GetFoodWithoutFeedback(int patientId)
        {
            var results = await _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId && ce.FueRealizado == false && ce.FechaEvento < DateTime.Now)
                .Join(
                    _context.EventoComida,
                    ce => ce.Id,
                    ec => ec.IdCargaEvento,
                    (ce, ec) => new FoodSummary
                    {
                        EventId = ce.Id,
                        EventDate = ce.FechaEvento,
                        Carbohydrates = ec.Carbohidratos,
                        KindEventId = ce.IdTipoEvento 
                    }
                )
                .OrderBy(ce => ce.EventDate)
                .ToListAsync();

            return results;
        }

        // -------------------------------------------------------- ⬇⬇ Physical Activity Feedback ⬇⬇ -------------------------------------------------------
        public async Task<List<PhysicalActivitySummary>> GetPhysicalActivityWithoutFeedback(int patientId)
        {
            var results = await _context.CargaEventos
                .Where(ce => ce.IdPaciente == patientId && ce.FueRealizado == false && ce.FechaEvento < DateTime.Now)
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
                    (combined, af) => new PhysicalActivitySummary
                    {
                        EventId = combined.ce.Id,
                        EventDate = combined.ce.FechaEvento,
                        ActivityName = af.Nombre,
                        KindEventId = combined.ce.IdTipoEvento
                    }
                )
                .OrderBy(summary => summary.EventDate)
                .ToListAsync();

            return results;
        }
    }
}