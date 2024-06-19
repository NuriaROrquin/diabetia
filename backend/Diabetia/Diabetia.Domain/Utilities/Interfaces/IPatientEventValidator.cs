using Diabetia.Domain.Models;

namespace Diabetia.Interfaces
{
    public interface IPatientEventValidator
    {
        public Task ValidatePatientEvent(string email, CargaEvento @event);
    }
}
