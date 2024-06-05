using FakeItEasy;
using Diabetia.Domain.Services;
using Diabetia.Domain.Repositories;
using Diabetia.Application.UseCases;
using Diabetia.Interfaces;
using Diabetia.Application.Exceptions;

namespace Diabetia.Test.Core
{
    public class AuthRegisterUseCaseTest
    {
        [Fact]
        public async Task RegisterUseCase_WhenCalledWithValidData_ShouldRegisterUserSuccessfully()
        {
            var username = "testUser";
            var password = "testPassword";
            var email = "test@user.com";
            var hashCode = "hashtest";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(true);
            A.CallTo(() => fakeAuthProvider.RegisterUserAsync(username, password, email))
            .Returns(Task.FromResult(hashCode));

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

            // Act
            await registerUseCase.Register(username, email, password);

            // Asserts
            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthProvider.RegisterUserAsync(username, password, email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthRepository.SaveUserHashAsync(username, email, hashCode)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthRepository.SaveUserUsernameAsync(email, username)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task RegisterUseCase_GivenInvalidEmail_ThrowsInvalidEmailException()
        {
            var username = "testUser";
            var password = "testPassword";
            var email = "testuser.com";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(false);

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);
            
            // Act & Assert
            await Assert.ThrowsAsync<InvalidEmailException>(() => registerUseCase.Register(username,email,password));
            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RegisterUseCase_GivenAlreadyExistsEmail_ThrowsEmailAlreadyExistsException()
        {
            var username = "testUser";
            var password = "testPassword";
            var email = "testuser@gmail.com";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(true);
            A.CallTo(() => fakeAuthRepository.CheckEmailOnDatabaseAsync(email)).Returns(true);

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

            // Act & Assert
            await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => registerUseCase.Register(username, email, password));
            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthRepository.CheckEmailOnDatabaseAsync(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RegisterUseCase_GivenEmailCode_ShouldConfirmUserSuccessfully()
        {
            var username = "testUser";
            var email = "test@user.com";
            var hashCode = "hashtest";
            var confirmationCode = "123456";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(true);

            A.CallTo(() => fakeAuthRepository.GetUserHashAsync(email)).Returns(Task.FromResult(hashCode));

            A.CallTo(() => fakeAuthProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode)).Returns(Task.FromResult(true));

            A.CallTo(() => fakeAuthRepository.SetUserStateActiveAsync(email));

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

            // Act
            await registerUseCase.ConfirmEmailVerification(username, email, confirmationCode);

            // Asserts
            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeAuthProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode)).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeAuthRepository.GetUserHashAsync(email)).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeAuthRepository.SetUserStateActiveAsync(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RegisterUseCase_GivenEmailInvalidEmail_ThrowsInvalidEmailException()
        {
            var username = "testUser";
            var email = "test@user.com";
            var confirmationCode = "123456";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(false);

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidEmailException>(() => registerUseCase.ConfirmEmailVerification(username, email, confirmationCode));

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RegisterUseCase_GivenValidEmailNotExistsOnDatabase_ThrowsInvalidOperationException()
        {
            var username = "testUser";
            var email = "test@user.com";
            var confirmationCode = "123456";
            var hashCode = "";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(true);

            A.CallTo(() => fakeAuthRepository.GetUserHashAsync(email)).Returns(hashCode);

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => registerUseCase.ConfirmEmailVerification(username, email, confirmationCode));

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeAuthRepository.GetUserHashAsync(email)).MustHaveHappenedOnceExactly();

        }
    }
}
