using Diabetia.Application.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Interfaces;
using System.Data.Entity;

namespace Diabetia.Common.Utilities
{
    public class PatientValidator : IPatientValidator
    {
        private diabetiaContext _context;

        public PatientValidator(diabetiaContext context)
        {
            _context = context;
        }

        public async Task ValidatePatient(string email) {

            var patient = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdUsuarioNavigation.Email == email);

            if (patient == null)
            {
                throw new PatientNotFoundException();
            }

        }
    }
}
