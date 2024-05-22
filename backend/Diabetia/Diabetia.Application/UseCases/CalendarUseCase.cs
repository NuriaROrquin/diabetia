using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class CalendarUseCase
    {
        private readonly IUserRepository _userRepository;

        public CalendarUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsByEmail(string email)
        {
            var events = await _userRepository.GetAllEvents(email);

            return events;
        }
    }
}
