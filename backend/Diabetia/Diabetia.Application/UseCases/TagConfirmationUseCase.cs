using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class TagConfirmationUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly FoodManuallyUseCase _eventFoodUseCase;

        public TagConfirmationUseCase(IUserRepository userRepository, FoodManuallyUseCase eventFoodUseCase)
        {
            _userRepository = userRepository;
            _eventFoodUseCase = eventFoodUseCase;
        }

        public async Task<FoodResultsEvent> CalculateFoodResponseAsync(string email, List<NutritionTag> nutritionTags)
        {
            var userPatientInfo = await _userRepository.GetPatientInfo(email);

            FoodResultsEvent responses = new FoodResultsEvent();
 
            float totalChConsumed = 0;

            foreach (var tag in nutritionTags)
            {
                float chInPortionConsumed = CalculateChPerPortion(tag);
                totalChConsumed += chInPortionConsumed;
            }

            float insulinToCorrect = CalculateInsulinToCorrect(totalChConsumed, (float)userPatientInfo.ChCorrection);

            responses.ChConsumed = (int)totalChConsumed;
            responses.InsulinRecomended = (float)Math.Round(insulinToCorrect, 2);

            return responses;
        }

        private float CalculateChPerPortion(NutritionTag tag)
        {
            return tag.ChInPortion * tag.Portion;
        }

        private float CalculateInsulinToCorrect(float totalChConsumed, float chCorrection)
        {
            return (float)totalChConsumed / (float)chCorrection;
        }
    }
}
