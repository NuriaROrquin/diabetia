using Diabetia.API.DTO.EventRequest.Food;
using Diabetia.API.DTO.EventRequest.Glucose;
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

            var carbohidrates = await _foodManuallyUseCase.AddFoodManuallyEventAsync(email, request.ToDomain());

            return Ok(carbohidrates);
        }

        /*
        [HttpPost("EditFoodManuallyEvent")]
        public async Task<IActionResult> EditMedicalEventAsync([FromBody] EditFoodManuallyRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var foodManually = request.ToDomain();
            await _foodManuallyUseCase.EditFoodManuallyEventAsync(email, foodManually);
            return Ok("Comida modificada correctamente");
        }*/
    }
}
