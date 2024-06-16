using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Services;
using Diabetia.Interfaces;
using FakeItEasy;
using Diabetia.Domain.Models;
using Diabetia.Domain.Utilities.Interfaces;
using Diabetia.Application.UseCases.AuthUseCases;

namespace Diabetia_Core.Auth
{
    public class AuthForgotPasswordUseCaseTest
    {
        [Fact]
        public async Task ForgotPasswordUseCase_WhenCalledWithValidData_ShouldSendEmailSuccessfully()
        {
            // Arrange
            var email = "testEmail@gmail.com";
            var username = "testUsername";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };

            var state = true;
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeUsernameDBValidator = A.Fake<IUsernameDBValidator>();
            var fakeUserStatusValidator = A.Fake<IUserStatusValidator>();

            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).Returns(username);
            A.CallTo(() => fakeAuthProvider.ForgotPasswordRecoverAsync(username));

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeEmailValidator, fakeUsernameDBValidator, fakeUserStatusValidator);

            // Act
            await forgotPasswordUseCase.ForgotPasswordEmailAsync(user);

            // Asserts
            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUserStatusValidator.CheckUserStatus(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthProvider.ForgotPasswordRecoverAsync(username)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ForgotPasswordUseCase_GivenInvalidEmail_ShouldThrowInvalidEmailException()
        {
            // Arrange
            var email = "invalidEmail";
            var username = "fakeUsername";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeUsernameDBValidator = A.Fake<IUsernameDBValidator>();
            var fakeUserStatusValidator = A.Fake<IUserStatusValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Throws<InvalidEmailException>();
            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeEmailValidator, fakeUsernameDBValidator, fakeUserStatusValidator);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidEmailException>(() => forgotPasswordUseCase.ForgotPasswordEmailAsync(user));

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task ForgotPasswordUseCase_WhenGivenNotRegisteredEmail_ShouldThrowUsernameNotFoundException()
        {
            // Arrange
            var email = "email.test@gmail.com";
            var username = "";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeUsernameDBValidator = A.Fake<IUsernameDBValidator>();
            var fakeUserStatusValidator = A.Fake<IUserStatusValidator>();

            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).Throws<UsernameNotFoundException>();

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeEmailValidator, fakeUsernameDBValidator, fakeUserStatusValidator);

            // Act & Assert
            await Assert.ThrowsAsync<UsernameNotFoundException>(() => forgotPasswordUseCase.ForgotPasswordEmailAsync(user));

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ForgotPasswordUseCase_WhenUserIsNotConfirmed_ShouldThrowUserNotAuthorizedException()
        {
            // Arrange
            var email = "email.test@gmail.com";
            var username = "testUsername";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeUsernameDBValidator = A.Fake<IUsernameDBValidator>();
            var fakeUserStatusValidator = A.Fake<IUserStatusValidator>();


            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).Returns(username);
            A.CallTo(() => fakeUserStatusValidator.CheckUserStatus(user.Email)).Throws<UserNotAuthorizedException>();

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeEmailValidator, fakeUsernameDBValidator, fakeUserStatusValidator);

            // Act & Assert
            await Assert.ThrowsAsync<UserNotAuthorizedException>(() => forgotPasswordUseCase.ForgotPasswordEmailAsync(user));

            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUserStatusValidator.CheckUserStatus(user.Email)).MustHaveHappenedOnceExactly();
        }


        // Confirm Forgot Password Test

        [Fact]
        public async Task ForgotPasswordUseCase_WhenCalledWithValidData_ShouldChangePasswordSuccessfully()
        {
            // Arrange
            var email = "testEmail@gmail.com";
            var username = "testUsername";
            var confirmationCode = "123456";
            var password = "testPassword";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeUsernameDBValidator = A.Fake<IUsernameDBValidator>();
            var fakeUserStatusValidator = A.Fake<IUserStatusValidator>();

            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).Returns(username);
            A.CallTo(() => fakeAuthProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password));

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeEmailValidator, fakeUsernameDBValidator, fakeUserStatusValidator);

            // Act
            await forgotPasswordUseCase.ConfirmForgotPasswordAsync(user, confirmationCode, password);

            // Asserts
            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task ForgotPasswordUseCase_GivenNotValidEmail_ShouldThrowInvalidEmailException()
        {
            // Arrange
            var email = "testUsername";
            var confirmationCode = "123456";
            var password = "testPassword";
            var username = "fakeUsername";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeUsernameDBValidator = A.Fake<IUsernameDBValidator>();
            var fakeUserStatusValidator = A.Fake<IUserStatusValidator>();

            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).Throws<InvalidEmailException>();

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeEmailValidator, fakeUsernameDBValidator, fakeUserStatusValidator);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidEmailException>(() => forgotPasswordUseCase.ConfirmForgotPasswordAsync(user, confirmationCode, password));
            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ForgotPasswordUseCase_GivenNotRegisteredUsername_ShouldThrowUsernameNotFoundException()
        {
            // Arrange
            var email = "testEmail@gmail.com";
            var confirmationCode = "123456";
            var password = "testPassword";
            var username = "fakeUsername";
            var user = new Usuario()
            {
                Email = email,
                Username = username,
            };
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeEmailValidator = A.Fake<IEmailValidator>();
            var fakeUsernameDBValidator = A.Fake<IUsernameDBValidator>();
            var fakeUserStatusValidator = A.Fake<IUserStatusValidator>();


            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).Throws<UsernameNotFoundException>();

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeEmailValidator, fakeUsernameDBValidator, fakeUserStatusValidator);

            // Act & Assert
            await Assert.ThrowsAsync<UsernameNotFoundException>(() => forgotPasswordUseCase.ConfirmForgotPasswordAsync(user, confirmationCode, password));
            A.CallTo(() => fakeEmailValidator.IsValidEmail(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeUsernameDBValidator.CheckUsernameOnDB(user.Email)).MustHaveHappenedOnceExactly();
        }
    }
}
