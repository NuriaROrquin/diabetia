using Diabetia.API.DTO.ReportingResponse;
using Diabetia.Application.UseCases.ReportingUseCases;
using Diabetia.Domain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Reporting
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FoodReportController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FoodReportUseCase _foodReportUseCase;

        public FoodReportController(IHttpContextAccessor httpContextAccessor, FoodReportUseCase foodReportUseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _foodReportUseCase = foodReportUseCase;
        }

        [HttpGet("GetFoodSummaryEventReport")]
        public async Task<IActionResult> ShowFoodSummaryEventToReporting([FromQuery] DateFilter request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var @events = await _foodReportUseCase.GetFoodToReporting(email, request.DateFrom.Value, request.DateTo.Value);
            var physicalActivitiesResponse = events.Select(e => PhysicalActivityAmountResponse.FromObject(e)).ToList();

            return Ok(physicalActivitiesResponse);
        }
    }
}
