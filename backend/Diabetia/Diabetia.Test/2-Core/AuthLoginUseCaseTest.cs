using Amazon.CognitoIdentityProvider.Model;
using Diabetia.Application.Exceptions;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Infrastructure.Repositories;
using FakeItEasy;

namespace Diabetia_Core
{
    public class AuthLoginUseCaseTest
    {
        [Fact]
        public async Task AuthLoginUseCase_GivenValidCredentials_ShouldLoginSuccessfully()
        {
            // Arrange
            var username = "testUsername";
            var password = "testPassword";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeUserRepository = A.Fake<IUserRepository>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
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

            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(username)).Returns(true);

            A.CallTo(() => fakeAuthProvider.LoginUserAsync(username, password)).Returns(Task.FromResult(expectedTokenResponse));

            A.CallTo(() => fakeUserRepository.GetUserInformationFromUsernameAsync(username)).Returns(Task.FromResult(expectedUser));

            A.CallTo(() => fakeUserRepository.GetStatusInformationCompletedAsync(username)).Returns(true);


            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository);

            // Act
            var user = await userLoginUseCase.UserLoginAsync(username, password);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(expectedTokenResponse.AuthenticationResult.AccessToken, user.Token);
            A.CallTo(() => fakeAuthProvider.LoginUserAsync(username, password)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AuthLoginUseCase_GivenInvalidUsername_ThrowsUserNotFoundException()
        {
            // Arrange
            var username = "invalidUsername";
            var password = "testPassword";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeUserRepository = A.Fake<IUserRepository>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(username)).Returns(false);

            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository);

            // Assert
            await Assert.ThrowsAsync<UsernameNotFoundException>(() => userLoginUseCase.UserLoginAsync(username, password));
        }

        [Fact]
        public async Task AuthLoginUseCase_GivenInvalidUsernameOrPassword_ThrowsUserNotAuthorizedException()
        {
            // Arrange
            var username = "invalidUsername";
            var password = "testPassword";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeUserRepository = A.Fake<IUserRepository>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(username)).Returns(true);

            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository);

            // Assert % Act
            await Assert.ThrowsAsync<UserNotAuthorizedException>(() => userLoginUseCase.UserLoginAsync(username, password));
        }

        [Fact]
        public async Task AuthLoginUseCase_GivenValidUsernameNotInformation_ThrowsNoInformationUserException()
        {
            // Arrange
            var username = "invalidUsername";
            var password = "testPassword";
            var expectedTokenResponse = new InitiateAuthResponse
            {
                AuthenticationResult = new AuthenticationResultType
                {
                    AccessToken = "fakeAccessToken"
                }
            };

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeUserRepository = A.Fake<IUserRepository>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(username)).Returns(true);

            A.CallTo(() => fakeAuthProvider.LoginUserAsync(username, password)).Returns(Task.FromResult(expectedTokenResponse));

            A.CallTo(() => fakeUserRepository.GetUserInformationFromUsernameAsync(username)).Returns<User>(null);

            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository);

            // Assert & Act
            await Assert.ThrowsAsync<NoInformationUserException>(() => userLoginUseCase.UserLoginAsync(username, password));
        }
    }
}
