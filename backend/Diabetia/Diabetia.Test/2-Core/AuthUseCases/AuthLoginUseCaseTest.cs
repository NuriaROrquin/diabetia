using Amazon.CognitoIdentityProvider.Model;
using Diabetia.Application.Exceptions;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Infrastructure.Repositories;
using Diabetia.Interfaces;
using FakeItEasy;

namespace Diabetia_Core.Auth
{
    public class AuthLoginUseCaseTest
    {
        [Fact]
        public async Task AuthLoginUseCase_GivenValidEmail_ShouldLoginSuccessfully()
        {
            // Arrange
            var userInput = "testEmail@gmail.com";
            var password = "testPassword";
            var username = "testUsername";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeUserRepository = A.Fake<IUserRepository>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeInputValidator = A.Fake<IInputValidator>();

            var expectedTokenResponse = new InitiateAuthResponse
            {
                AuthenticationResult = new AuthenticationResultType
                {
                    AccessToken = "fakeAccessToken"
                }
            };
            var expectedUser = new User
            {
                Email = "testEmail@gmail.com"
            };
            A.CallTo(() => fakeInputValidator.IsEmail(userInput)).Returns(true);
            A.CallTo(() => fakeAuthRepository.GetUsernameByEmailAsync(userInput)).Returns(username);
            A.CallTo(() => fakeAuthProvider.LoginUserAsync(username, password)).Returns(Task.FromResult(expectedTokenResponse));
            A.CallTo(() => fakeUserRepository.GetUserInformationFromUsernameAsync(username)).Returns(Task.FromResult(expectedUser));
            A.CallTo(() => fakeUserRepository.GetStatusInformationCompletedAsync(username)).Returns(true);


            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository, fakeInputValidator);

            // Act
            var user = await userLoginUseCase.UserLoginAsync(userInput, password);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(expectedTokenResponse.AuthenticationResult.AccessToken, user.Token);
            A.CallTo(() => fakeAuthProvider.LoginUserAsync(username, password)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthLoginUseCase_GivenValidUsername_ShouldLoginSuccessfully()
        {
            // Arrange
            var userInput = "username";
            var password = "testPassword";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeUserRepository = A.Fake<IUserRepository>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeInputValidator = A.Fake<IInputValidator>();

            var expectedTokenResponse = new InitiateAuthResponse
            {
                AuthenticationResult = new AuthenticationResultType
                {
                    AccessToken = "fakeAccessToken"
                }
            };
            var expectedUser = new User
            {
                Email = "testEmail@gmail.com"
            };
            A.CallTo(() => fakeInputValidator.IsEmail(userInput)).Returns(false);
            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(userInput)).Returns(true);
            A.CallTo(() => fakeAuthProvider.LoginUserAsync(userInput, password)).Returns(Task.FromResult(expectedTokenResponse));
            A.CallTo(() => fakeUserRepository.GetUserInformationFromUsernameAsync(userInput)).Returns(Task.FromResult(expectedUser));
            A.CallTo(() => fakeUserRepository.GetStatusInformationCompletedAsync(userInput)).Returns(true);


            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository, fakeInputValidator);

            // Act
            var user = await userLoginUseCase.UserLoginAsync(userInput, password);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(expectedTokenResponse.AuthenticationResult.AccessToken, user.Token);
            A.CallTo(() => fakeAuthProvider.LoginUserAsync(userInput, password)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthLoginUseCase_GivenInvalidUsername_ThrowsUserNotFoundException()
        {
            // Arrange
            var userInput = "invalidUsername";
            var password = "testPassword";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeUserRepository = A.Fake<IUserRepository>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeInputValidator = A.Fake<IInputValidator>();

            A.CallTo(() => fakeInputValidator.IsEmail(userInput)).Returns(false);
            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(userInput)).Returns(false);

            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository, fakeInputValidator);

            // Assert & Act
            await Assert.ThrowsAsync<UsernameNotFoundException>(() => userLoginUseCase.UserLoginAsync(userInput, password));
        }

        [Fact]
        public async Task AuthLoginUseCase_GivenInvalidUsernameOrPassword_ThrowsUserNotAuthorizedException()
        {
            // Arrange
            var userInput = "invalidUsername";
            var password = "InvalidPassword";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeUserRepository = A.Fake<IUserRepository>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeInputValidator = A.Fake<IInputValidator>();

            A.CallTo(() => fakeInputValidator.IsEmail(userInput)).Returns(false);
            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(userInput)).Returns(true);

            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository, fakeInputValidator);

            // Assert & Act
            await Assert.ThrowsAsync<UserNotAuthorizedException>(() => userLoginUseCase.UserLoginAsync(userInput, password));
        }

        [Fact]
        public async Task AuthLoginUseCase_GivenValidUsernameNotInformation_ThrowsNoInformationUserException()
        {
            // Arrange
            var userInput = "validUsername";
            var password = "testPassword";
            var expectedTokenResponse = new InitiateAuthResponse
            {
                AuthenticationResult = new AuthenticationResultType
                {
                    AccessToken = "fakeAccessToken"
                }
            };

            var user = new User
            {
                Id = 1,
                Email = "test@test.com",
                Username = userInput
            };

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeUserRepository = A.Fake<IUserRepository>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
            var fakeInputValidator = A.Fake<IInputValidator>();

            A.CallTo(() => fakeInputValidator.IsEmail(userInput)).Returns(false);
            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(userInput)).Returns(true);
            A.CallTo(() => fakeAuthProvider.LoginUserAsync(userInput, password)).Returns(Task.FromResult(expectedTokenResponse));
            A.CallTo(() => fakeUserRepository.GetUserInformationFromUsernameAsync(userInput)).Returns<User>(user);

            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository, fakeInputValidator);

            // Assert & Act
            await Assert.ThrowsAsync<NoInformationUserException>(() => userLoginUseCase.UserLoginAsync(userInput, password));
        }
    }
}
