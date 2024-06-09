using Diabetia.Domain.Repositories;

namespace Diabetia.Application.UseCases
{
    public class AddGlucoseEventUseCase
    {
        private readonly IEventRepository _eventRepository;

        public AddGlucoseEventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task AddGlucoseEvent(string Email, int KindEvent, DateTime EventDate, String FreeNote, decimal Glucose, int? IdDevicePacient, int? IdFoodEvent, bool? PostFoodMedition)
        {
            await _eventRepository.AddGlucoseEvent(Email, KindEvent, EventDate, FreeNote, Glucose, IdDevicePacient, IdFoodEvent, PostFoodMedition);
        }
    }
}
