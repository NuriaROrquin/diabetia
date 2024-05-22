using Microsoft.AspNetCore.Mvc;
using Diabetia.Application.UseCases;
using Diabetia.API.DTO;
using Diabetia.Domain.Entities;
using Amazon.Runtime.Internal;


namespace Diabetia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarUseCase _calendarUseCase;
        public CalendarController(CalendarUseCase calendarUseCase)
        {
            _calendarUseCase = calendarUseCase;
        }

        [HttpPost("getAllEvents")]
        public async Task<IEnumerable<Event>> GetAllEvents([FromBody] string email)
        {
            var events = await _calendarUseCase.GetAllEventsByEmail(email);

            return events;
        }

    }
}
