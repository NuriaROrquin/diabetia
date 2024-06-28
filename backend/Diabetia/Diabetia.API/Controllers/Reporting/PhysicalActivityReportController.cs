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
    public class PhysicalActivityReportController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PhysicalActivityReportUseCase _physicalActivityAmountReportUseCase;


        public PhysicalActivityReportController(IHttpContextAccessor httpContextAccessor, PhysicalActivityReportUseCase physicalActivityAmountReportUseCase) 
        {
            _httpContextAccessor = httpContextAccessor;
            _physicalActivityAmountReportUseCase = physicalActivityAmountReportUseCase;

        }


        [HttpGet("GetPhysicalActivityReport")]
        public async Task<IActionResult> ShowPhysicalActivityReporting([FromQuery] DateFilter request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var @events = await _physicalActivityAmountReportUseCase.GetPhysicalActivityToReporting(email, request.DateFrom.Value, request.DateTo.Value);
            var physicalActivitiesResponse = events.Select(e => PhysicalActivityAmountResponse.FromObject(e)).ToList();

            return Ok(physicalActivitiesResponse);
        }
    }
}
