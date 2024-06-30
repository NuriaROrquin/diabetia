using Diabetia.Domain.Entities;
using Diabetia.Domain.Entities.Events;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Refit;

namespace Diabetia.Application.UseCases
{
    public class FoodDishDetectionUseCase
    {
        private readonly IFoodDishProvider _foodDishProvider;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public FoodDishDetectionUseCase(IFoodDishProvider foodDishProvider, IUserRepository userRepository, IEventRepository eventRepository)
        {
            _foodDishProvider = foodDishProvider;
            _userRepository = userRepository;
            _eventRepository = eventRepository; 
        }

        public async Task<FoodDish> DetectFoodDish(FoodDish foodImageBase64)
        {
            byte[] imageBytes = Convert.FromBase64String(foodImageBase64.ImageBase64);
        
            using var imageStream = new MemoryStream(imageBytes);

            var streamPart = new StreamPart(imageStream, "image.jpg", "image/jpeg");

            var foodDish = await _foodDishProvider.DetectFoodDish(streamPart);

            return foodDish;
        }

        public async Task<IngredientsDetected> ConfirmDish(FoodDish foodDish)
        {
            var nutrientsDetected = await _foodDishProvider.GetNutrientPerIngredient(foodDish);

            return nutrientsDetected;
        }

        public async Task<FoodResultsEvent> SaveFoodEvent(string email, List<FoodInfo> ingredientsConfirmed)
        {
            var userPatientInfo = await _userRepository.GetPatientInfo(email);

            FoodResultsEvent responses = new FoodResultsEvent();

            float totalChConsumed = 0;

            foreach (var tag in ingredientsConfirmed)
            {
                float chInPortionConsumed = CalculateChPerPortion(tag);
                totalChConsumed += chInPortionConsumed;
            }

            float insulinToCorrect = CalculateInsulinToCorrect(totalChConsumed, (float)userPatientInfo.ChCorrection);

            responses.ChConsumed = (int)totalChConsumed;
            responses.InsulinRecomended = (float)Math.Round(insulinToCorrect, 2);

            await _eventRepository.AddFoodByDetectionEvent(userPatientInfo.Id, DateTime.Now, totalChConsumed);

            return responses;
        }

        private float CalculateChPerPortion(FoodInfo tag)
        {
            return (float)tag.Carbohydrates * tag.Quantity;
        }

        private float CalculateInsulinToCorrect(float totalChConsumed, float chCorrection)
        {
            if (chCorrection == 0)
            {
                throw new DivideByZeroException();
            }
            return (float)totalChConsumed / (float)chCorrection;
        }
    }
}

