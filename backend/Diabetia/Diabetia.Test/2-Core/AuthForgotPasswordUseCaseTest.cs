using Diabetia.Application.Exceptions;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using FakeItEasy;

namespace Diabetia.Test._2_Core
{
    public class AuthForgotPasswordUseCaseTest
    {
        [Fact]
        public async Task ForgotPasswordUseCase_WhenCalledWithValidData_ShouldSendEmailSuccessfully()
        {
            // Arrange
            var email = "testEmail@gmail.com";
            var username = "testUsername";
            var state = true;
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).Returns(username);
            A.CallTo(() => fakeAuthRepository.GetUserStateAsync(email)).Returns(state);

            A.CallTo(() => fakeAuthProvider.ForgotPasswordRecoverAsync(username));

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act
            await forgotPasswordUseCase.ForgotPasswordEmailAsync(email);

            // Asserts
            A.CallTo(() => fakeAuthProvider.ForgotPasswordRecoverAsync(username)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ForgotPasswordUseCase_WhenCalledWithInvalidEmail_ShouldThrowInvalidEmailException()
        {
            // Arrange
            var invalidEmail = "invalidEmail";
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidEmailException>(() => forgotPasswordUseCase.ForgotPasswordEmailAsync(invalidEmail));
        }

        [Fact]
        public async Task ForgotPasswordUseCase_WhenUserIsNotConfirmed_ShouldThrowUserNotAuthorizedException()
        {
            // Arrange
            var email = "test.email@gmail.com";
            var username = "testUsername";
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).Returns(username);
            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act & Assert
            await Assert.ThrowsAsync<UserNotAuthorizedException>(() => forgotPasswordUseCase.ForgotPasswordEmailAsync(email));
        }

        [Fact]
        public async Task ForgotPasswordUseCase_WhenGivenNotRegisteredEmail_ShouldThrowUsernameNotFoundException()
        {
            // Arrange
            var email = "email.test@gmail.com";
            var state = true;
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            A.CallTo(() => fakeAuthRepository.GetUserStateAsync(email)).Returns(state);
            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act & Assert
            await Assert.ThrowsAsync<UsernameNotFoundException>(() => forgotPasswordUseCase.ForgotPasswordEmailAsync(email));
        }

        // Confirm Forgot Password Test

        [Fact]
        public async Task ForgotPasswordUseCase_WhenCalledWithValidData_ShouldChangePasswordSuccessfully()
        {
            // Arrange
            var username = "testUsername";
            var confirmationCode = "123456";
            var password = "testPassword";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(username)).Returns(true);

            A.CallTo(() => fakeAuthProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password));

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act
            await forgotPasswordUseCase.ConfirmForgotPasswordAsync(username, confirmationCode, password);

            // Asserts
            A.CallTo(() => fakeAuthProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ForgotPasswordUseCase_GivenNotRegisteredUsername_ShouldThrowUsernameNotFoundException()
        {
            // Arrange
            var username = "testUsername";
            var confirmationCode = "123456";
            var password = "testPassword";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act & Assert
            await Assert.ThrowsAsync<UsernameNotFoundException>(() => forgotPasswordUseCase.ConfirmForgotPasswordAsync(username, confirmationCode, password));
        }
    }
}
