using FakeItEasy;
using Diabetia.Domain.Services;
using Diabetia.Domain.Repositories;
using Diabetia.Interfaces;
using Diabetia.Domain.Exceptions;
using Diabetia.Application.UseCases.AuthUseCases;
using Diabetia.Domain.Utilities.Interfaces;
using Diabetia.Domain.Models;

namespace Diabetia_Core.Auth
{
    public class AuthRegisterUseCaseTest
    {
        // Register Test

        [Fact]
        public async Task RegisterUseCase_WhenCalledWithValidData_ShouldRegisterUserSuccessfully()
        {
            var username = "testUser";
            var password = "testPassword";
            var email = "test@user.com";
            var hashCode = "hashtest";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeEmailDBValidator = A.Fake<IEmailDBValidator>();
            var fakeHashValidator = A.Fake<IHashValidator>();

            A.CallTo(() => fakeAuthProvider.RegisterUserAsync(user, password)).Returns(Task.FromResult(hashCode));

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator, fakeEmailDBValidator, fakeHashValidator);

            // Act
            await registerUseCase.Register(user, password);

            // Asserts
            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEmailDBValidator.CheckEmailOnDB(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthProvider.RegisterUserAsync(user, password)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthRepository.SaveUserHashAsync(user, hashCode)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthRepository.SaveUserUsernameAsync(user)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task RegisterUseCase_GivenInvalidEmail_ThrowsInvalidEmailException()
        {
            var username = "testUser";
            var password = "testPassword";
            var email = "testuser.com";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeEmailDBValidator = A.Fake<IEmailDBValidator>();
            var fakeHashValidator = A.Fake<IHashValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).Throws<InvalidEmailException>();

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator, fakeEmailDBValidator, fakeHashValidator);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidEmailException>(() => registerUseCase.Register(user, password));
            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RegisterUseCase_GivenAlreadyExistsEmail_ThrowsEmailAlreadyExistsException()
        {
            var username = "testUser";
            var password = "testPassword";
            var email = "testuser@gmail.com";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeEmailDBValidator = A.Fake<IEmailDBValidator>();
            var fakeHashValidator = A.Fake<IHashValidator>();

            A.CallTo(() => fakeEmailDBValidator.CheckEmailOnDB(user.Email)).Throws<EmailAlreadyExistsException>();

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator, fakeEmailDBValidator, fakeHashValidator);

            // Act & Assert
            await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => registerUseCase.Register(user, password));
            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeEmailDBValidator.CheckEmailOnDB(user.Email)).MustHaveHappenedOnceExactly();
        }

        // Confirm Email Test
        [Fact]
        public async Task RegisterUseCase_GivenEmailCode_ShouldConfirmUserSuccessfully()
        {
            var hashCode = "hashtest";
            var confirmationCode = "123456";
            var user = new Usuario()
            {
                Email = "test@user.com",
                Username = "testUser",
            };

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeEmailDBValidator = A.Fake<IEmailDBValidator>();
            var fakeHashValidator = A.Fake<IHashValidator>();

            A.CallTo(() => fakeHashValidator.GetUserHash(user.Email)).Returns(hashCode);
            A.CallTo(() => fakeAuthProvider.ConfirmEmailVerificationAsync(user.Username, hashCode, confirmationCode)).Returns(true);

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator, fakeEmailDBValidator, fakeHashValidator);

            // Act
            await registerUseCase.ConfirmEmailVerification(user, confirmationCode);

            // Asserts
            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeHashValidator.GetUserHash(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthProvider.ConfirmEmailVerificationAsync(user.Username, hashCode, confirmationCode)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthRepository.SetUserStateActiveAsync(user.Email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RegisterUseCase_GivenEmailInvalidEmail_ThrowsInvalidEmailException()
        {
            var confirmationCode = "123456";
            var user = new Usuario()
            {
                Email = "test@user.com",
                Username = "testUser",
            };

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeEmailDBValidator = A.Fake<IEmailDBValidator>();
            var fakeHashValidator = A.Fake<IHashValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).Throws<InvalidEmailException>();

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator, fakeEmailDBValidator, fakeHashValidator);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidEmailException>(() => registerUseCase.ConfirmEmailVerification(user, confirmationCode));
            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RegisterUseCase_GivenValidEmailNotExistsHashCodeOnDatabase_ThrowsInvalidOperationException()
        {
            var confirmationCode = "123456";
            var user = new Usuario()
            {
                Email = "test@user.com",
                Username = "testUser",
            };

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeEmailDBValidator = A.Fake<IEmailDBValidator>();
            var fakeHashValidator = A.Fake<IHashValidator>();

            A.CallTo(() => fakeHashValidator.GetUserHash(user.Email)).Throws<InvalidOperationException>();

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator, fakeEmailDBValidator, fakeHashValidator);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => registerUseCase.ConfirmEmailVerification(user, confirmationCode));

            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeHashValidator.GetUserHash(user.Email)).MustHaveHappenedOnceExactly();

        }
    }
}
