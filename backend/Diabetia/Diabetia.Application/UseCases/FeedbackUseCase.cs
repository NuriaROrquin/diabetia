using Diabetia.Domain.Models;
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

        public async Task<List<Dictionary<string, object>>> GetEventsToFeedback(string email)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email);
            var listEventsWithoutFeedback = await _feedbackRepository.GetAllEventsWithoutFeedback(patient.Id);
            if (listEventsWithoutFeedback == null || listEventsWithoutFeedback.Count == 0)
            {
                return new List<Dictionary<string, object>>();
            }
            return listEventsWithoutFeedback;
        }

        public async Task AddFeedbackAsync(string email, Feedback feedback)
        {
            await _patientValidator.ValidatePatient(email);
            await _feedbackRepository.AddFeedback(feedback);
        }
    }
}
