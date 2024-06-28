using Diabetia.API.DTO.ReportingResponse.Glucose;
using Diabetia.Application.UseCases.ReportingUseCases;
using Diabetia.Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Diabetia.API.Controllers.Reporting
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GlucoseReportController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GlucoseReportUseCase _glucoseReportUseCase;

        public GlucoseReportController(IHttpContextAccessor httpContextAccessor, GlucoseReportUseCase glucoseReportUseCase)
        {
            _httpContextAccessor = httpContextAccessor;
            _glucoseReportUseCase = glucoseReportUseCase;
        }

        [HttpGet("GetGlucoseSummaryEventReport")]
        public async Task<IActionResult> ShowGlucoseSummaryEventToReporting([FromQuery] DateFilter request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var @events = await _glucoseReportUseCase.GetGlucoseToReporting(email, request.DateFrom.Value, request.DateTo.Value);
            var glucoseResponse = events.Select(e => GlucoseResponse.FromObject(e)).ToList();

            return Ok(glucoseResponse);
        }

        [HttpGet("GetGlucoseHyperglycemiaReport")]
        public async Task<IActionResult> ShowHyperglycemiaGlucoseMeasurementToReporting()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var @events = await _glucoseReportUseCase.GetHyperglycemiaGlucoseToReporting(email);
            var glucoseResponse = events.Select(e => GlucoseMeasurementResponse.FromObject(e)).ToList();

            return Ok(glucoseResponse);
        }

        [HttpGet("GetGlucoseHypoglycemiaReport")]
        public async Task<IActionResult> ShowHypoglycemiaGlucoseMeasurementToReporting()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var @events = await _glucoseReportUseCase.GetHypoglycemiaGlucoseToReporting(email);
            var glucoseResponse = events.Select(e => GlucoseMeasurementResponse.FromObject(e)).ToList();

            return Ok(glucoseResponse);
        }
    }
}
