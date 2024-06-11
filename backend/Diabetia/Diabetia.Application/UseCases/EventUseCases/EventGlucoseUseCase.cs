using Diabetia.Domain.Repositories;

namespace Diabetia.Application.UseCases.EventUseCases
{
    public class EventGlucoseUseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventGlucoseUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task AddGlucoseEvent(string Email, int KindEvent, DateTime EventDate, String FreeNote, decimal Glucose, int? IdFoodEvent, bool? PostFoodMedition)
        {
            await _eventRepository.AddGlucoseEvent(Email, KindEvent, EventDate, FreeNote, Glucose, IdFoodEvent, PostFoodMedition);
        }

        public async Task EditGlucoseEvent(int IdEvent, string Email, DateTime EventDate, String FreeNote, decimal Glucose, int? IdFoodEvent, bool? PostFoodMedition)
        {
            await _eventRepository.EditGlucoseEvent(IdEvent, Email, EventDate, FreeNote, Glucose, IdFoodEvent, PostFoodMedition);
        }

    }
}
