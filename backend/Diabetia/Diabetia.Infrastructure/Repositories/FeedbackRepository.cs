using Diabetia.Domain.Entities.Feedback;
using Diabetia.Domain.Repositories;
using Diabetia.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Diabetia.Infrastructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly diabetiaContext _context;
        public FeedbackRepository(diabetiaContext context) {
            _context = context;
        }

        // -------------------------------------------------------- ⬇⬇ Food Feedback ⬇⬇ -------------------------------------------------------
        public async Task <List<FoodSummary>> GetFoodWithoutFeedback(int patientId)
        {
            var results = await _context.CargaEventos
        .Where(ce => ce.IdPaciente == patientId && ce.FueRealizado == false && ce.FechaEvento < DateTime.Now)
        .Join(
            _context.EventoComida,
            ce => ce.Id,
            ec => ec.IdCargaEvento,
            (ce, ec) => new FoodSummary
            {
                EventDay = ce.FechaEvento,
                Carbohydrates = ec.Carbohidratos
            }
        )
        .OrderBy(ce => ce.EventDay)
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
                    EventDay = combined.ce.FechaEvento,
                    ActivityName = af.Nombre
                }
            )
            .OrderBy(summary => summary.EventDay)
            .ToListAsync();

                return results;
        }
    }
}
