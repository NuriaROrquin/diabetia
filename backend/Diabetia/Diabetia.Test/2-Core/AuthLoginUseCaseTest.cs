using Amazon.CognitoIdentityProvider.Model;
using Diabetia.Application.Exceptions;
using Diabetia.Application.UseCases;
using Diabetia.Domain.Entities;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using Diabetia.Infrastructure.Repositories;
using FakeItEasy;

namespace Diabetia.Test._2_Core
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
            }; ;
            var expectedUser = new User
            {
                Email = "testEmail@gmail.com"
            };

            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(username)).Returns(true);

            A.CallTo(() => fakeAuthProvider.LoginUserAsync(username, password)).Returns(Task.FromResult(expectedTokenResponse)); ;

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
            //var expectedTokenResponse = new InitiateAuthResponse
            //{
            //    AuthenticationResult = new AuthenticationResultType
            //    {
            //        AccessToken = "fakeAccessToken"
            //    }
            //}; ;
            //var expectedUser = new User
            //{
            //    Email = "testEmail@gmail.com"
            //};

            A.CallTo(() => fakeAuthRepository.CheckUsernameOnDatabaseAsync(username)).Returns(false);

            //A.CallTo(() => fakeAuthProvider.LoginUserAsync(username, password)).Returns(Task.FromResult(expectedTokenResponse)); ;

            //A.CallTo(() => fakeUserRepository.GetUserInformationFromUsernameAsync(username)).Returns(Task.FromResult(expectedUser));

            //A.CallTo(() => fakeUserRepository.GetStatusInformationCompletedAsync(username)).Returns(true);


            var userLoginUseCase = new AuthLoginUseCase(fakeAuthProvider, fakeUserRepository, fakeAuthRepository);

            // Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => userLoginUseCase.UserLoginAsync(username, password));
        }
    }
}
