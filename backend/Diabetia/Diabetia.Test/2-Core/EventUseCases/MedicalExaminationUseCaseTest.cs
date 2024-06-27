using Diabetia.Application.UseCases.EventUseCases;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia.Test._2_Core.EventUseCases
{
    public class MedicalExaminationUseCaseTest
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPatientValidator _patientValidator;
        private readonly IUserRepository _userRepository;
        private readonly IPatientEventValidator _patientEventValidator;
        private readonly ITagRecognitionProvider _tagRecognitionProvider;
        private readonly MedicalExaminationUseCase _fakeMedicalExaminationUseCase;
        public MedicalExaminationUseCaseTest() 
        {
            _eventRepository = A.Fake<IEventRepository>();
            _patientValidator = A.Fake<IPatientValidator>();
            _userRepository = A.Fake<IUserRepository>();
            _patientEventValidator = A.Fake<IPatientEventValidator>();
            _tagRecognitionProvider = A.Fake<ITagRecognitionProvider>();
            _fakeMedicalExaminationUseCase = new MedicalExaminationUseCase(_eventRepository,_patientValidator,_userRepository,_patientEventValidator,_tagRecognitionProvider);
        }

        // --------------------------------------- ⬇⬇ Add Medical Examination Event ⬇⬇ ---------------------------------------
        [Fact]
        public async Task EventMedicalExaminationUseCase_WhenCalledWithValidData_ShouldAddEventSuccessfully()
        {
            // Arrange
            var email = "testEmail@gmail.com";
            var patient = new Paciente() { Id = 1 };
            var medicalExamination = new EventoEstudio() { IdProfesional = 1, Archivo = "TestBase64File", TipoEstudio = "TestFile" };
            var fakeFileSavedId = "1";

            A.CallTo(() => _userRepository.GetPatient(email)).Returns(patient);
            A.CallTo(() => _tagRecognitionProvider.SaveMedicalExaminationOnBucket(medicalExamination.Archivo)).Returns(fakeFileSavedId);

            // Act
            await _fakeMedicalExaminationUseCase.AddMedicalExaminationEventAsync(email, medicalExamination);

            // Assert
            A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _tagRecognitionProvider.SaveMedicalExaminationOnBucket(medicalExamination.Archivo)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _eventRepository.AddMedicalExaminationEventAsync(patient.Id, medicalExamination, fakeFileSavedId)).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task EventMedicalExaminationUseCase_WhenCalledWithInvalidPatient_ThrowsPatientNotFoundException()
        {
            // Arrange
            var email = "testEmail@gmail.com";
            var patient = new Paciente() { Id = 1 };
            var medicalExamination = new EventoEstudio() { IdProfesional = 1, Archivo = "TestBase64File", TipoEstudio = "TestFile" };

            A.CallTo(() => _patientValidator.ValidatePatient(email)).Throws<PatientNotFoundException>();

            // Act & Assert
            await Assert.ThrowsAsync<PatientNotFoundException>(() => _fakeMedicalExaminationUseCase.AddMedicalExaminationEventAsync(email, medicalExamination));
            A.CallTo(() => _patientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
        }

    }
}
