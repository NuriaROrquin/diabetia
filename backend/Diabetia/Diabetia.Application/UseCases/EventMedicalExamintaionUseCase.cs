using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Application.UseCases
{
    public class EventMedicalExamintaionUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITagRecognitionProvider _tagRecognitionProvider;

        public EventMedicalExamintaionUseCase(IEventRepository eventRepository, ITagRecognitionProvider tagRecognitionProvider)
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
