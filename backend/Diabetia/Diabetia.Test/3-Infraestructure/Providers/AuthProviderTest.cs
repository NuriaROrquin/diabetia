using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Diabetia.Domain.Services;
using FakeItEasy;
using Infrastructure.Provider;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Diabetia.Test.Infraestructure.Providers
{
    public class AuthProviderTest
    {
        [Fact]
        public async Task RegisterUserAsync_Success_ReturnsSecretHash()
        {
            var username = "testUser";
            var password = "testPassword";
            var email = "test@user.com";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var clientId = "clientId";
            var clientSecret = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            var request = new SignUpRequest
            {
                ClientId = fakeConfiguration["ClientId"],
                Password = password,
                SecretHash = authProvider.CalculateSecretHash(clientId, clientSecret, username),
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType { Name = "email", Value = email}
                },
                Username = username
            };

            A.CallTo(() => fakeCognitoClient.SignUpAsync(request, CancellationToken.None));
            var result = await authProvider.RegisterUserAsync(username, password, email);

            Assert.NotNull(result);
            Assert.Equal(request.SecretHash, result);

            A.CallTo(() => fakeCognitoClient.SignUpAsync(
                A<SignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Password == password &&
                    req.SecretHash == authProvider.CalculateSecretHash(clientId, clientSecret, username) &&
                    req.UserAttributes.Any(attr => attr.Name == "email" && attr.Value == email) &&
                    req.Username == username
                ),
                CancellationToken.None
            )).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task RegisterUserAsync_WhenUserAlreadyExists_ThrowsUsernameExistsException()
        {
            // Arrange
            var username = "testUser";
            var password = "testPassword";
            var email = "test@user.com";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var clientId = "clientId";
            var clientSecret = "clientSecret";
            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.SignUpAsync(A<SignUpRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UsernameExistsException("El usuario ya está registrado"));

            // Act y Assert
            var exception = await Assert.ThrowsAsync<UsernameExistsException>(() =>
            authProvider.RegisterUserAsync(username, password, email));

            Assert.Equal("El usuario ya está registrado", exception.Message);
            A.CallTo(() => fakeCognitoClient.SignUpAsync(
                A<SignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Password == password &&
                    req.SecretHash == authProvider.CalculateSecretHash(clientId, clientSecret, username) &&
                    req.UserAttributes.Any(attr => attr.Name == "email" && attr.Value == email) &&
                    req.Username == username
                ),
                CancellationToken.None
            )).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RegisterUserAsync_WhenGivenPasswordNotValid_ThrowsPasswordInvalidException()
        {
            // Arrange
            var username = "testUser";
            var password = "testPassword";
            var email = "test@user.com";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var clientId = "clientId";
            var clientSecret = "clientSecret";
            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.SignUpAsync(A<SignUpRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new InvalidPasswordException("Contraseña inválida"));

            // Act y Assert
            var exception = await Assert.ThrowsAsync<InvalidPasswordException>(() =>
            authProvider.RegisterUserAsync(username, password, email));

            Assert.Equal("Contraseña inválida", exception.Message);
            A.CallTo(() => fakeCognitoClient.SignUpAsync(
                A<SignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Password == password &&
                    req.SecretHash == authProvider.CalculateSecretHash(clientId, clientSecret, username) &&
                    req.UserAttributes.Any(attr => attr.Name == "email" && attr.Value == email) &&
                    req.Username == username
                ),
                CancellationToken.None
            )).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RegisterUserAsync_WhenGivenParameterNotValid_ThrowsInvalidParameterException()
        {
            // Arrange
            var username = "testUser";
            var password = "testPassword";
            var email = "test@user.com";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var clientId = "clientId";
            var clientSecret = "clientSecret";
            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.SignUpAsync(A<SignUpRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new InvalidParameterException("Parámetros de solicitud inválidos"));

            // Act y Assert
            var exception = await Assert.ThrowsAsync<InvalidParameterException>(() =>
            authProvider.RegisterUserAsync(username, password, email));

            Assert.Equal("Parámetros de solicitud inválidos", exception.Message);
            A.CallTo(() => fakeCognitoClient.SignUpAsync(
                A<SignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Password == password &&
                    req.SecretHash == authProvider.CalculateSecretHash(clientId, clientSecret, username) &&
                    req.UserAttributes.Any(attr => attr.Name == "email" && attr.Value == email) &&
                    req.Username == username
                ),
                CancellationToken.None
            )).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmEmailAsync_GivenCodeSentToAccountEmail_ConfirmsEmail()
        {
            // Arrange
            var username = "testUser";
            var password = "testPassword";
            var email = "test@user.com";
            var confirmationCode = "123456";
            var hashCode = "hashTest";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var clientId = "clientId";
            var clientSecret = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            var request = new ConfirmSignUpRequest
            {
                ClientId = fakeConfiguration["ClientId"],
                Username = username,
                ConfirmationCode = confirmationCode,
                SecretHash = hashCode
            };

            var response = new ConfirmSignUpResponse
            {
                HttpStatusCode = HttpStatusCode.OK
            };

            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(
                A<ConfirmSignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Username == username &&
                    req.ConfirmationCode == confirmationCode &&
                    req.SecretHash == hashCode),
                CancellationToken.None))
                .Returns(Task.FromResult(response));

            // Act
            var result = await authProvider.ConfirmEmailVerificationAsync(username,hashCode, confirmationCode);

            // Assert
            Assert.True(result);
            Assert.NotNull(result);
            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(
                A<ConfirmSignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Username == username &&
                    req.ConfirmationCode == confirmationCode &&
                    req.SecretHash == hashCode), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmEmailAsync_GivenExpiredCode_ThrowsExpiredCodeException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "123456";
            var hashCode = "hashTest";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);


            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(A<ConfirmSignUpRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new ExpiredCodeException("Código vencido"));

            // Act y Assert
            var exception = await Assert.ThrowsAsync<ExpiredCodeException>(() =>
            authProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode));

            Assert.Equal("Código vencido", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(
                A<ConfirmSignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Username == username &&
                    req.ConfirmationCode == confirmationCode &&
                    req.SecretHash == hashCode
                ),CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmEmailAsync_GivenInvalidUser_ThrowsUserNotFoundException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "123456";
            var hashCode = "hashTest";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);


            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(A<ConfirmSignUpRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotFoundException("Usuario no encontrado"));

            // Act y Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(() =>
            authProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode));

            Assert.Equal("Usuario no encontrado", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(
                A<ConfirmSignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Username == username &&
                    req.ConfirmationCode == confirmationCode &&
                    req.SecretHash == hashCode
                ), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmEmailAsync_GivenInvalidParameters_ThrowsInvalidParameterException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "123456";
            var hashCode = "hashTest";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(A<ConfirmSignUpRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new InvalidParameterException("Parametros incorrectos"));

            // Act y Assert
            var exception = await Assert.ThrowsAsync<InvalidParameterException>(() =>
            authProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode));

            Assert.Equal("Parametros incorrectos", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(
                A<ConfirmSignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Username == username &&
                    req.ConfirmationCode == confirmationCode &&
                    req.SecretHash == hashCode
                ), CancellationToken.None)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task ConfirmEmailAsync_GivenInvalidCode_ThrowsCodeMismatchException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "123456";
            var hashCode = "hashTest";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(A<ConfirmSignUpRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new CodeMismatchException("Código incorrecto"));

            // Act y Assert
            var exception = await Assert.ThrowsAsync<CodeMismatchException>(() =>
            authProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode));

            Assert.Equal("Código incorrecto", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(
                A<ConfirmSignUpRequest>.That.Matches(req =>
                    req.ClientId == fakeConfiguration["ClientId"] &&
                    req.Username == username &&
                    req.ConfirmationCode == confirmationCode &&
                    req.SecretHash == hashCode
                ), CancellationToken.None)).MustHaveHappenedOnceExactly();

        }
    }
}