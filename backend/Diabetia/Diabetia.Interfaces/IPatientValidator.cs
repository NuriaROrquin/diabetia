
namespace Diabetia.Interfaces
{
    public interface IPatientValidator
    {
        public Task ValidatePatient(string email);
    }
}