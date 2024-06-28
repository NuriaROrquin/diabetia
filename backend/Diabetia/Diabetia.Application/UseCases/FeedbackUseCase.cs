using Diabetia.Domain.Utilities.Validations;

namespace Diabetia.Application.UseCases
{
    public class FeedbackUseCase
    {
        private readonly PatientValidator _patientValidator;

        public FeedbackUseCase(PatientValidator patientValidator)
        {
            _patientValidator = patientValidator;
        }
    }
}
