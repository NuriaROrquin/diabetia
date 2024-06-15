//using Diabetia.Domain.Exceptions;
//using Diabetia.Application.UseCases;
//using Diabetia.Domain.Repositories;
//using Diabetia.Domain.Services;
//using Diabetia.Interfaces;
//using FakeItEasy;

//namespace Diabetia_Core.Auth
//{
//    public class AuthForgotPasswordUseCaseTest
//    {
//        [Fact]
//        public async Task ForgotPasswordUseCase_WhenCalledWithValidData_ShouldSendEmailSuccessfully()
//        {
//            // Arrange
//            var email = "testEmail@gmail.com";
//            var username = "testUsername";
//            var state = true;
//            var fakeAuthProvider = A.Fake<IAuthProvider>();
//            var fakeAuthRepository = A.Fake<IAuthRepository>();
//            var fakeEmailValidator = A.Fake<IEmailValidator>();

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(true);
//            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).Returns(username);
//            A.CallTo(() => fakeAuthRepository.GetUserStateAsync(email)).Returns(state);

//            A.CallTo(() => fakeAuthProvider.ForgotPasswordRecoverAsync(username));

//            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

//            // Act
//            await forgotPasswordUseCase.ForgotPasswordEmailAsync(email);

//            // Asserts
//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();

//            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).MustHaveHappenedOnceExactly();

//            A.CallTo(() => fakeAuthRepository.GetUserStateAsync(email)).MustHaveHappenedOnceExactly();

//            A.CallTo(() => fakeAuthProvider.ForgotPasswordRecoverAsync(username)).MustHaveHappenedOnceExactly();
//        }

//        [Fact]
//        public async Task ForgotPasswordUseCase_GivenInvalidEmail_ShouldThrowInvalidEmailException()
//        {
//            // Arrange
//            var email = "invalidEmail";
//            var fakeAuthProvider = A.Fake<IAuthProvider>();
//            var fakeAuthRepository = A.Fake<IAuthRepository>();
//            var fakeEmailValidator = A.Fake<IEmailValidator>();

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(false);

//            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

//            // Act & Assert
//            await Assert.ThrowsAsync<InvalidEmailException>(() => forgotPasswordUseCase.ForgotPasswordEmailAsync(email));

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();

//        }

//        [Fact]
//        public async Task ForgotPasswordUseCase_WhenGivenNotRegisteredEmail_ShouldThrowUsernameNotFoundException()
//        {
//            // Arrange
//            var email = "email.test@gmail.com";
//            var username = "";
//            var fakeAuthProvider = A.Fake<IAuthProvider>();
//            var fakeAuthRepository = A.Fake<IAuthRepository>();
//            var fakeEmailValidator = A.Fake<IEmailValidator>();

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(true);

//            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).Returns(username);

//            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

//            // Act & Assert
//            await Assert.ThrowsAsync<UsernameNotFoundException>(() => forgotPasswordUseCase.ForgotPasswordEmailAsync(email));

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();

//            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).MustHaveHappenedOnceExactly();
//        }

//        [Fact]
//        public async Task ForgotPasswordUseCase_WhenUserIsNotConfirmed_ShouldThrowUserNotAuthorizedException()
//        {
//            // Arrange
//            var email = "email.test@gmail.com";
//            var username = "testUsername";
//            var state = false;
//            var fakeAuthProvider = A.Fake<IAuthProvider>();
//            var fakeAuthRepository = A.Fake<IAuthRepository>();
//            var fakeEmailValidator = A.Fake<IEmailValidator>();

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(true);

//            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).Returns(username);

//            A.CallTo(() => fakeAuthRepository.GetUserStateAsync(email)).Returns(state);

//            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

//            // Act & Assert
//            await Assert.ThrowsAsync<UserNotAuthorizedException>(() => forgotPasswordUseCase.ForgotPasswordEmailAsync(email));

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).MustHaveHappenedOnceExactly();

//            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).MustHaveHappenedOnceExactly();

//            A.CallTo(() => fakeAuthRepository.GetUserStateAsync(email)).MustHaveHappenedOnceExactly();
//        }


//        // Confirm Forgot Password Test

//        [Fact]
//        public async Task ForgotPasswordUseCase_WhenCalledWithValidData_ShouldChangePasswordSuccessfully()
//        {
//            // Arrange
//            var email = "testEmail@gmail.com";
//            var username = "testUsername";
//            var confirmationCode = "123456";
//            var password = "testPassword";

//            var fakeAuthProvider = A.Fake<IAuthProvider>();
//            var fakeAuthRepository = A.Fake<IAuthRepository>();
//            var fakeEmailValidator = A.Fake<IEmailValidator>();

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(true);
//            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).Returns(username);
//            A.CallTo(() => fakeAuthProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password));

//            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

//            // Act
//            await forgotPasswordUseCase.ConfirmForgotPasswordAsync(email, confirmationCode, password);

//            // Asserts
//            A.CallTo(() => fakeAuthProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password)).MustHaveHappenedOnceExactly();
//        }

//        [Fact]
//        public async Task ForgotPasswordUseCase_GivenNotValidEmail_ShouldThrowInvalidEmailException()
//        {
//            // Arrange
//            var email = "testUsername";
//            var confirmationCode = "123456";
//            var password = "testPassword";

//            var fakeAuthProvider = A.Fake<IAuthProvider>();
//            var fakeAuthRepository = A.Fake<IAuthRepository>();
//            var fakeEmailValidator = A.Fake<IEmailValidator>();

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(false);

//            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

//            // Act & Assert
//            await Assert.ThrowsAsync<InvalidEmailException>(() => forgotPasswordUseCase.ConfirmForgotPasswordAsync(email, confirmationCode, password));
//        }

//        [Fact]
//        public async Task ForgotPasswordUseCase_GivenNotRegisteredUsername_ShouldThrowUsernameNotFoundException()
//        {
//            // Arrange
//            var email = "testEmail@gmail.com";
//            var username = "";
//            var confirmationCode = "123456";
//            var password = "testPassword";

//            var fakeAuthProvider = A.Fake<IAuthProvider>();
//            var fakeAuthRepository = A.Fake<IAuthRepository>();
//            var fakeEmailValidator = A.Fake<IEmailValidator>();

//            A.CallTo(() => fakeEmailValidator.IsValidEmail(email)).Returns(true);
//            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(email)).Returns(username);

//            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository, fakeEmailValidator);

//            // Act & Assert
//            await Assert.ThrowsAsync<UsernameNotFoundException>(() => forgotPasswordUseCase.ConfirmForgotPasswordAsync(email, confirmationCode, password));
//        }
//    }
//}
