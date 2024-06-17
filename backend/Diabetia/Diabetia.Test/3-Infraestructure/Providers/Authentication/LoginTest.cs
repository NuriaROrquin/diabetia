using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using FakeItEasy;
using Infrastructure.Provider;
using Microsoft.Extensions.Configuration;

namespace Diabetia.Test._3_Infraestructure.Providers.Authentication
{
    public class LoginTest
    {
        [Fact]
        public async Task LoginUserAsync_GivenValidCredentials_ShouldLoginSuccessfully()
        {
            // Arrange
            var username = "testUsername";
            var password = "testPassword";

            var fakeConfigurarion = A.Fake<IConfiguration>();
            fakeConfigurarion["ClientId"] = "testClientId";
            fakeConfigurarion["ClientSecret"] = "testClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfigurarion, fakeCognitoClient);

            var request = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", username },
                    { "PASSWORD", password },
                    { "SECRET_HASH", authProvider.CalculateSecretHash(fakeConfigurarion["ClientId"], fakeConfigurarion["ClientSecret"], username) }
                },
                ClientId = fakeConfigurarion["ClientId"]
            };
            A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(request, CancellationToken.None));

            // Act
            var response = await authProvider.LoginUserAsync(username, password);

            // Assert
            Assert.NotNull(response);
            A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.That.Matches(req =>
                req.AuthFlow == AuthFlowType.USER_PASSWORD_AUTH &&
                req.AuthParameters["USERNAME"] == username &&
                req.AuthParameters["PASSWORD"] == password &&
                req.ClientId == fakeConfigurarion["ClientId"] &&
                req.AuthParameters["SECRET_HASH"] == authProvider.CalculateSecretHash(fakeConfigurarion["ClientId"], fakeConfigurarion["ClientSecret"], username)),
                CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task LoginUserAsync_GivenNotConfirmUser_ThrowsUserNotConfirmedException()
        {
            // Arrange
            var username = "testUsername";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "testClientId";
            fakeConfiguration["ClientSecret"] = "testClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotConfirmedException("El usuario no esta confirmado"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotConfirmedException>(() => authProvider.LoginUserAsync(username, password));

            Assert.Equal("El usuario no esta confirmado", exception.Message);
            A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.That.Matches(req =>
                req.AuthFlow == AuthFlowType.USER_PASSWORD_AUTH &&
                req.AuthParameters["USERNAME"] == username &&
                req.AuthParameters["PASSWORD"] == password &&
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.AuthParameters["SECRET_HASH"] == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)),
                CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task LoginUserAsync_GivenCaducatedPassword_ThrowsPasswordResetRequiredException()
        {
            // Arrange
            var username = "testUsername";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "testClientId";
            fakeConfiguration["ClientSecret"] = "testClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new PasswordResetRequiredException("La contraseña se encuentra vencida"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<PasswordResetRequiredException>(() => authProvider.LoginUserAsync(username, password));

            Assert.Equal("La contraseña se encuentra vencida", exception.Message);
            A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.That.Matches(req =>
                req.AuthFlow == AuthFlowType.USER_PASSWORD_AUTH &&
                req.AuthParameters["USERNAME"] == username &&
                req.AuthParameters["PASSWORD"] == password &&
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.AuthParameters["SECRET_HASH"] == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)),
                CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task LoginUserAsync_GivenInvalidUsername_ThrowsUserNotFoundException()
        {
            // Arrange
            var username = "testUsername";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "testClientId";
            fakeConfiguration["ClientSecret"] = "testClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotFoundException("Usuario no encontrado"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(() => authProvider.LoginUserAsync(username, password));

            Assert.Equal("Usuario no encontrado", exception.Message);
            A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.That.Matches(req =>
                req.AuthFlow == AuthFlowType.USER_PASSWORD_AUTH &&
                req.AuthParameters["USERNAME"] == username &&
                req.AuthParameters["PASSWORD"] == password &&
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.AuthParameters["SECRET_HASH"] == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)),
                CancellationToken.None)).MustHaveHappenedOnceExactly();
        }
    }
}
