
using Diabetia.Domain.Repositories;

namespace Diabetia.Application.UseCases.EventUseCases
{
    public class EventMedicalVisitUseCase
    {
        private readonly IEventRepository _eventRepository;

        public EventMedicalVisitUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddMedicalVisitEventAsync(string Email, int KindEventId, DateTime VisitDate, int ProfessionalId, bool Recordatory, DateTime RecordatoryDate, string description)
        {
            await _eventRepository.AddMedicalVisitEventAsync(Email, KindEventId, VisitDate, ProfessionalId, Recordatory, RecordatoryDate, description);
        }
    }
}
