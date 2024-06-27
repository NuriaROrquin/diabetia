using Amazon.Runtime.Internal;
using Diabetia.API.DTO;
using Diabetia.API.DTO.EventRequest;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diabetia.API.Controllers.Calendar
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CalendarController : ControllerBase
    {
        private readonly ILogger<CalendarController> _logger;

        private readonly CalendarUseCase _calendarUseCase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CalendarController(ILogger<CalendarController> logger, CalendarUseCase calendarUseCase, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _calendarUseCase = calendarUseCase;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("events")]
        public async Task<Dictionary<string, List<EventItem>>> GetAllEvents()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var eventsByDate = await _calendarUseCase.GetAllEvents(email);
            return eventsByDate;
        }

        [HttpGet("eventsByDate")]
        public async Task<IEnumerable<EventItem>> GetEventsByDate([FromQuery] BasicEventRequest request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var eventsByDate = await _calendarUseCase.GetAllEventsByDate(request.EventDate, email);
            return eventsByDate;
        }

    }
}

