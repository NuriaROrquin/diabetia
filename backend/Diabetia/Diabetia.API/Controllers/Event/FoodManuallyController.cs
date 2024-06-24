using Diabetia.API.DTO.EventRequest.Food;
using Diabetia.API.DTO.EventResponse.Food;
using Diabetia.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Event
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FoodManuallyController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FoodManuallyUseCase _foodManuallyUseCase;

        public FoodManuallyController(IHttpContextAccessor httpContextAccessor, FoodManuallyUseCase foodManuallyUseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _foodManuallyUseCase = foodManuallyUseCase;
        }

        [HttpPost("AddFoodManuallyEvent")]
        public async Task<IActionResult> AddFoodManuallyAsync([FromBody] AddFoodManuallyRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var foodEventResponse = await _foodManuallyUseCase.AddFoodManuallyEventAsync(email, request.ToDomain());

            var response = new FoodResponse();
            response.ChConsumed = foodEventResponse.ChConsumed;
            response.InsulinRecomended = foodEventResponse.InsulinRecomended;

            return Ok(response);
        }

        
        [HttpPost("EditFoodManuallyEvent")]
        public async Task<IActionResult> EditMedicalEventAsync([FromBody] EditFoodManuallyRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var response = new FoodResponse();

            var foodEventResponse = await _foodManuallyUseCase.EditFoodManuallyEventAsync(email, request.ToDomain());

            response.ChConsumed = foodEventResponse.ChConsumed;
            response.InsulinRecomended = foodEventResponse.InsulinRecomended;

            return Ok(response);
        }
    }
}
