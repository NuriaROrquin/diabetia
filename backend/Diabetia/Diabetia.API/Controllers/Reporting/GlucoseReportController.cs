using Diabetia.API.DTO.ReportingResponse;
using Diabetia.Application.UseCases.ReportingUseCases;
using Diabetia.Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Reporting
{
    public class GlucoseReportController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GlucoseReportUseCase _glucoseReportUseCase;

        public GlucoseReportController(IHttpContextAccessor httpContextAccessor, GlucoseReportUseCase glucoseReportUseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _glucoseReportUseCase = glucoseReportUseCase;
        }

        [HttpGet("GetGlucoseReport")]
        public async Task<IActionResult> ShowGlucoseReporting([FromQuery] DateFilter request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var @events = await _glucoseReportUseCase.GetGlucoseToReporting(email, request.DateFrom.Value, request.DateTo.Value);
            var physicalActivitiesResponse = events.Select(e => GlucoseResponse.FromObject(e)).ToList();

            return Ok(physicalActivitiesResponse);
        }
    }
}
