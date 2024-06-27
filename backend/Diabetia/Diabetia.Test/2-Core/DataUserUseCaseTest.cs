using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Models;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.DataUser
{
    public class DataUserUseCaseTests
    {
        private readonly IUserRepository _fakeUserRepository;
        private readonly IPatientValidator _fakePatientValidator;
        private readonly DataUserUseCase _dataUserUseCase;

        public DataUserUseCaseTests()
        {
            _fakeUserRepository = A.Fake<IUserRepository>();
            _fakePatientValidator = A.Fake<IPatientValidator>();
            _dataUserUseCase = new DataUserUseCase(_fakeUserRepository, _fakePatientValidator);
        }

        [Fact]
        public async Task GetUserInfo_ShouldReturnUserInfo()
        {
            // Arrange
            var userName = "testUser";
            var user = new Usuario();
            A.CallTo(() => _fakeUserRepository.GetUserInfo(userName)).Returns(user);

            // Act
            var result = await _dataUserUseCase.GetUserInfo(userName);

            // Assert
            Assert.Equal(user, result);
            A.CallTo(() => _fakeUserRepository.GetUserInfo(userName)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task FirstStep_ShouldCompleteUserInfoAndReturnPatient()
        {
            // Arrange
            var email = "test@example.com";
            var patient = new Paciente();
            var patientLocal = new Paciente();
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).Returns(patientLocal);

            // Act
            var result = await _dataUserUseCase.FirstStep(email, patient);

            // Assert
            Assert.Equal(patientLocal, result);
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.CompleteUserInfo(patient)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.GetPatient(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task SecondStep_ShouldUpdateUserInfo()
        {
            // Arrange
            var email = "test@example.com";
            var patient = new Paciente();
            var patientInsuline = new InsulinaPaciente();

            // Act
            await _dataUserUseCase.SecondStep(email, patient, patientInsuline);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.UpdateUserInfo(patient, patientInsuline)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ThirdStep_ShouldCompletePhysicalUserInfo()
        {
            // Arrange
            var email = "test@example.com";
            var patientActfisica = new PacienteActividadFisica();

            // Act
            await _dataUserUseCase.ThirdStep(email, patientActfisica);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.CompletePhysicalUserInfo(patientActfisica)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task FourthStep_ShouldCompleteDeviceslUserInfo()
        {
            // Arrange
            var email = "test@example.com";
            var patientDispo = new DispositivoPaciente();
            var tieneDispositivo = true;

            // Act
            await _dataUserUseCase.FourthStep(email, patientDispo, tieneDispositivo);

            // Assert
            A.CallTo(() => _fakePatientValidator.ValidatePatient(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeUserRepository.CompleteDeviceslUserInfo(patientDispo, tieneDispositivo)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetEditUserInfo_ShouldReturnEditUserInfo()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User();
            A.CallTo(() => _fakeUserRepository.GetEditUserInfo(email)).Returns(user);

            // Act
            var result = await _dataUserUseCase.GetEditUserInfo(email);

            // Assert
            Assert.Equal(user, result);
            A.CallTo(() => _fakeUserRepository.GetEditUserInfo(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetPatientInfo_ShouldReturnPatientInfo()
        {
            // Arrange
            var email = "test@example.com";
            var patient = new Patient();
            A.CallTo(() => _fakeUserRepository.GetPatientInfo(email)).Returns(patient);

            // Act
            var result = await _dataUserUseCase.GetPatientInfo(email);

            // Assert
            Assert.Equal(patient, result);
            A.CallTo(() => _fakeUserRepository.GetPatientInfo(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetPhysicalInfo_ShouldReturnPhysicalInfo()
        {
            // Arrange
            var email = "test@example.com";
            var patient = new Patient();
            A.CallTo(() => _fakeUserRepository.GetPhysicalInfo(email)).Returns(patient);

            // Act
            var result = await _dataUserUseCase.GetPhysicalInfo(email);

            // Assert
            Assert.Equal(patient, result);
            A.CallTo(() => _fakeUserRepository.GetPhysicalInfo(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetExerciseInfo_ShouldReturnExerciseInfo()
        {
            // Arrange
            var email = "test@example.com";
            var exercisePatient = new Exercise_Patient();
            A.CallTo(() => _fakeUserRepository.GetExerciseInfo(email)).Returns(exercisePatient);

            // Act
            var result = await _dataUserUseCase.GetExerciseInfo(email);

            // Assert
            Assert.Equal(exercisePatient, result);
            A.CallTo(() => _fakeUserRepository.GetExerciseInfo(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetPatientDeviceInfo_ShouldReturnPatientDeviceInfo()
        {
            // Arrange
            var email = "test@example.com";
            var devicePatient = new Device_Patient();
            A.CallTo(() => _fakeUserRepository.GetPatientDeviceInfo(email)).Returns(devicePatient);

            // Act
            var result = await _dataUserUseCase.GetPatientDeviceInfo(email);

            // Assert
            Assert.Equal(devicePatient, result);
            A.CallTo(() => _fakeUserRepository.GetPatientDeviceInfo(email)).MustHaveHappenedOnceExactly();
        }
    }
}
