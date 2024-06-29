using Diabetia.Domain.Entities.Feedback;
using Diabetia.Domain.Entities.Reporting;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases
{
    public class FeedbackUseCase
    {
        private readonly IPatientValidator _patientValidator;
        private readonly IUserRepository _userRepository;
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackUseCase(IPatientValidator patientValidator, IUserRepository userRepository, IFeedbackRepository feedbackRepository)
        {
            _patientValidator = patientValidator;
            _userRepository = userRepository;
            _feedbackRepository = feedbackRepository;
        }

        public async Task<List<FoodSummary>> GetFoodToFeedback(string email)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            var listOfFoodConsumed = await _feedbackRepository.GetFoodWithoutFeedback(patient.Id);
            if (listOfFoodConsumed == null || listOfFoodConsumed.Count == 0)
            {
                return new List<FoodSummary>();
            }
            return listOfFoodConsumed;
        }

        public async Task<List<PhysicalActivitySummary>> GetPhysicalActivityToFeedback(string email)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            var listPhysicalActivities = await _feedbackRepository.GetPhysicalActivityWithoutFeedback(patient.Id);
            if (listPhysicalActivities == null || listPhysicalActivities.Count == 0)
            {
                return new List<PhysicalActivitySummary>();
            }
            return listPhysicalActivities;
        }
    }
}
