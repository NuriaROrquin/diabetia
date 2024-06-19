using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using FakeItEasy;
using Infrastructure.Provider;
using Microsoft.Extensions.Configuration;

namespace Diabetia.Test._3_Infraestructure.Providers.Authentication
{
    public class ChangePasswordTest
    {
        [Fact]
        public async Task ChangePasswordAsync_GivenPreviousAndNewPassword_ShouldChange()
        {
            // Arrange
            var accessToken = "testUser";
            var previousPassword = "testPassword";
            var newPassword = "testNewPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            var request = new ChangePasswordRequest
            {
                AccessToken = accessToken,
                PreviousPassword = previousPassword,
                ProposedPassword = newPassword
            };

            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(request, CancellationToken.None));

            // Act
            await authProvider.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword);

            // Assert & Act
            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(
                A<ChangePasswordRequest>.That.Matches(req =>
                    req.AccessToken == accessToken &&
                    req.PreviousPassword == previousPassword &&
                    req.ProposedPassword == newPassword), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ChangePasswordAsync_GivenInvalidNewPassword_ThrowInvalidPasswordException()
        {
            var accessToken = "testUser";
            var previousPassword = "testPassword";
            var newPassword = "testNewPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(A<ChangePasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new InvalidPasswordException("La contraseña ingresada no es válida."));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidPasswordException>(() => authProvider.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword));

            Assert.Equal("La contraseña ingresada no es válida.", exception.Message);
            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(
                A<ChangePasswordRequest>.That.Matches(req =>
                req.AccessToken == accessToken &&
                req.PreviousPassword == previousPassword &&
                req.ProposedPassword == newPassword), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ChangePasswordAsync_GivenInvalidUserInAccessToken_ThrowsUserNotFoundException()
        {
            var accessToken = "testUser";
            var previousPassword = "testPassword";
            var newPassword = "testNewPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(A<ChangePasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotFoundException("Usuario no encontrado."));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(() => authProvider.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword));

            Assert.Equal("Usuario no encontrado.", exception.Message);
            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(
                A<ChangePasswordRequest>.That.Matches(req =>
                req.AccessToken == accessToken &&
                req.PreviousPassword == previousPassword &&
                req.ProposedPassword == newPassword), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ChangePasswordAsync_WhenNotConfirmUser_ThrowsUserNotConfirmedException()
        {
            var accessToken = "testUser";
            var previousPassword = "testPassword";
            var newPassword = "testNewPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(A<ChangePasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotConfirmedException("El usuario aún no está confirmado."));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotConfirmedException>(() => authProvider.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword));

            Assert.Equal("El usuario aún no está confirmado.", exception.Message);
            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(
                A<ChangePasswordRequest>.That.Matches(req =>
                req.AccessToken == accessToken &&
                req.PreviousPassword == previousPassword &&
                req.ProposedPassword == newPassword), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ChangePasswordAsync_GivenInvalidPasswordsOrAccessToken_ThrowsInvalidParameterException()
        {
            var accessToken = "testUser";
            var previousPassword = "testPassword";
            var newPassword = "testNewPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(A<ChangePasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new InvalidParameterException("Uno de los datos no es válido"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidParameterException>(() => authProvider.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword));

            Assert.Equal("Uno de los datos no es válido", exception.Message);
            A.CallTo(() => fakeCognitoClient.ChangePasswordAsync(
                A<ChangePasswordRequest>.That.Matches(req =>
                req.AccessToken == accessToken &&
                req.PreviousPassword == previousPassword &&
                req.ProposedPassword == newPassword), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }
    }
}
