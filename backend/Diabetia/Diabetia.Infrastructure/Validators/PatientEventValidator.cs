using Diabetia.Application.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diabetia.Infrastructure.Validators
{
    public class PatientEventValidator : IPatientEventValidator
    {
        private diabetiaContext _context;

        public PatientEventValidator(diabetiaContext context)
        {
            _context = context;
        }

        public async Task ValidatePatientEvent(string email, CargaEvento @event)
        {
            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuarioNavigation.Email == email);
            if (patient.Id != @event.IdPaciente)
            {
                throw new EventNotRelatedWithPatientException();
            }
        }
    }
}
