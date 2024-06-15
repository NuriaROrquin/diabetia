using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;

namespace Diabetia.Application.UseCases
{
    public class EventMedicalExaminationUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITagRecognitionProvider _tagRecognitionProvider;

        public EventMedicalExaminationUseCase(IEventRepository eventRepository, ITagRecognitionProvider tagRecognitionProvider)
        {
            _eventRepository = eventRepository;
            _tagRecognitionProvider = tagRecognitionProvider;
        }

        public async Task AddMedicalExaminationEvent(string email, DateTime eventDate, string file, string examinationType, int? idProfessional, string? freeNote)
        {
            string fileSaved = await _tagRecognitionProvider.SaveMedicalExaminationOnBucket(file);
            await _eventRepository.AddMedicalExaminationEvent(email, eventDate, fileSaved, examinationType, idProfessional, freeNote);
        }

    }
}
