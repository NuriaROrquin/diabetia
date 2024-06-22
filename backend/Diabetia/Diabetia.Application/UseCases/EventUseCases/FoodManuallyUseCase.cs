using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Entities.Events;

namespace Diabetia.Application.UseCases.EventUseCases
{
    public class FoodManuallyUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;
        private readonly IPatientEventValidator _patientEventValidator;
        private readonly IUserRepository _userRepository;

        public FoodManuallyUseCase(IEventRepository eventRepository, IPatientValidator patientValidator, IPatientEventValidator patientEventValidator, IUserRepository userRepository)
        {
            _patientValidator = patientValidator;
            _eventRepository = eventRepository;
            _patientEventValidator = patientEventValidator;
            _userRepository = userRepository;
        }

        public Task AddFoodByTagEvent(string email, DateTime eventDate, int chConsumed)
        {
            throw new NotImplementedException();
        }

        public async Task<FoodResultsEvent> AddFoodManuallyEventAsync(string email, EventoComidum foodEvent)
        {
            var response = new FoodResultsEvent(); 
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatientInfo(email);
            response.ChConsumed = (int)await _eventRepository.AddFoodEventAsync(patient.Id, foodEvent);

            if (patient.ChCorrection != null)
            {
                float insulinToCorrect = (float)response.ChConsumed / (float)patient.ChCorrection;
                response.InsulinRecomended = insulinToCorrect;
            }

            return response;
        }
        
        public async Task<FoodResultsEvent> EditFoodManuallyEventAsync(string email, EventoComidum foodManually)
        {
            var response = new FoodResultsEvent();

            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatientInfo(email);
            var eventId = foodManually.IdCargaEventoNavigation.Id;
            var loadedEvent = await _eventRepository.GetEventByIdAsync(eventId);
            await _patientEventValidator.ValidatePatientEvent(email, loadedEvent);
            response.ChConsumed = (int)await _eventRepository.EditFoodEventAsync(foodManually);

            if (patient.ChCorrection != null)
            {
                float insulinToCorrect = (float)response.ChConsumed / (float)patient.ChCorrection;
                response.InsulinRecomended = insulinToCorrect;
            }

            return response;
        }
        /*
        public async Task AddFoodByTagEvent(string email, DateTime eventDate, int carbohydrates)
        {
            await _eventRepository.AddFoodByTagEvent(email, eventDate, carbohydrates);
        }*/
        
        public async Task<IEnumerable<AdditionalDataIngredient>> GetIngredients()
        {
            return await _eventRepository.GetIngredients();
        }
    }

    //// -------------------------------------------- ⬇️⬇ Food Manually ⬇️⬇ --------------------------------------------------
    //[HttpPost("AddFoodManuallyEvent")]
    //public async Task<EventFoodResponse> AddFoodManuallyEvent([FromBody] EventFoodRequest request)
    //{
    //    EventFoodResponse response = new EventFoodResponse();
    //   var totalChConsumed = await _eventFoodManuallyUseCase.AddFoodManuallyEvent(request.Email, request.EventDate, request.IdKindEvent.Value, request.Ingredients, request.FreeNote);

    //    var userPatientInfo = await _dataUserUseCase.GetPatientInfo(request.Email);
    //    if (userPatientInfo.ChCorrection != null)
    //    {
    //        var insulinToCorrect = totalChConsumed / userPatientInfo.ChCorrection;
    //        response.InsulinToCorrect = (float)insulinToCorrect;
    //    }

    //    response.ChConsumed = (int)totalChConsumed;

    //    return response;
    //}

    //[HttpPost("EditFoodManuallyEvent")]
    //public async Task<IActionResult> EditFoodManuallyEvent([FromBody] EventFoodRequest request)
    //{
    //    await _eventFoodManuallyUseCase.EditFoodManuallyEvent(request.IdEvent.Value, request.Email, request.EventDate, request.IdKindEvent.Value, request.Ingredients, request.FreeNote);
    //    return Ok();
    //}
}
