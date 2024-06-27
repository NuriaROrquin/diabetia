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
    public class ReportingController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly InsulinReportUseCase _insulinReportUseCase;
        public ReportingController(IHttpContextAccessor httpContextAccessor, InsulinReportUseCase insulineUseCase) 
        {
            _httpContextAccessor = httpContextAccessor;
            _insulinReportUseCase = insulineUseCase;
        }

        [HttpGet("GetInsulinReport")]
        public async Task <IActionResult> ShowInsulinReporting([FromQuery] DateFilter request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var @events = await _insulinReportUseCase.GetInsulinToReporting(email, request.DateFrom.Value, request.DateTo.Value);
            var insulinResponses = @events.Select(e => InsulinResponse.FromInsulinEvent(e)).ToList();
            return Ok(insulinResponses);
        }
    }
}
