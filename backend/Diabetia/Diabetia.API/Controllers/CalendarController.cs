
using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ILogger<CalendarController> _logger;

        private readonly CalendarUseCase _calendarUseCase;

        public CalendarController(ILogger<CalendarController> logger, CalendarUseCase calendarUseCase)
        {
            _logger = logger;
            _calendarUseCase = calendarUseCase;
        }

        [HttpPost("events")]
        public async Task<Dictionary<string, List<EventItem>>> GetAllEvents([FromBody] string email)
        {
            var eventsByDate = await _calendarUseCase.GetAllEvents(email);
            return eventsByDate;
        }

    }
}

