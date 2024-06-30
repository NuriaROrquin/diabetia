using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;

namespace Diabetia.Application.UseCases.EventUseCases
{
    public class MedicalExaminationUseCase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;
        private readonly IUserRepository _userRepository;
        private readonly IPatientEventValidator _patientEventValidator;
        private readonly ITagRecognitionProvider _tagRecognitionProvider;

        public MedicalExaminationUseCase(IEventRepository eventRepository, IPatientValidator patientValidator, IUserRepository userRepository, IPatientEventValidator patientEventValidator, ITagRecognitionProvider tagRecognitionProvider)
        {
            _eventRepository = eventRepository;
            _patientValidator = patientValidator;
            _userRepository = userRepository;
            _patientEventValidator = patientEventValidator;
            _tagRecognitionProvider = tagRecognitionProvider;
        }
        public async Task AddMedicalExaminationEventAsync(string email, EventoEstudio medicalExamination)
        {
            await _patientValidator.ValidatePatient(email);
            var patient = await _userRepository.GetPatient(email); //TODO: oport de mejora para retornar el paciente al mismo tiempo que se valida simil service
            string fileSavedId = await _tagRecognitionProvider.SaveMedicalExamination(medicalExamination.Archivo);
            await _eventRepository.AddMedicalExaminationEventAsync(patient.Id, medicalExamination, fileSavedId);
        }
    }
}
