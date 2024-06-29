using Diabetia.Domain.Entities.Feedback;

namespace Diabetia.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        public Task<List<Dictionary<string, object>>> GetAllEventsWithoutFeedback(int patientId);

        public Task<List<FoodSummary>> GetFoodWithoutFeedback(int patientId);

        public Task<List<PhysicalActivitySummary>> GetPhysicalActivityWithoutFeedback(int patientId);
    }
}
