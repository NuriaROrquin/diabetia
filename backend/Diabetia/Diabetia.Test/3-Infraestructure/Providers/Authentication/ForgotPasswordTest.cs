﻿using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using FakeItEasy;
using Infrastructure.Provider;
using Microsoft.Extensions.Configuration;

namespace Diabetia.Test._3_Infraestructure.Providers.Authentication
{
    public class ForgotPasswordTest
    {
        
        // Recover Password
        [Fact]
        public async Task ForgotPasswordRecoverAsync_GivenValidUser_ShouldSendCodeToEmail()
        {
            // Arrange
            var username = "testUsername";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            var request = new ForgotPasswordRequest
            {
                ClientId = fakeConfiguration["ClientId"],
                Username = username,
                SecretHash = authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)
            };

            A.CallTo(() => fakeCognitoClient.ForgotPasswordAsync(request, CancellationToken.None));

            // Act
            await authProvider.ForgotPasswordRecoverAsync(username);

            // Assert
            A.CallTo(() => fakeCognitoClient.ForgotPasswordAsync(A<ForgotPasswordRequest>.That.Matches(req =>
            req.ClientId == fakeConfiguration["ClientId"] &&
            req.Username == username &&
            req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ForgotPasswordRecoverAsync_GivenInvalidUser_ThrowsUserNotFoundException()
        {
            // Arrange
            var username = "testUsername";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ForgotPasswordAsync(A<ForgotPasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotFoundException("Usuario no encontrado"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(() => authProvider.ForgotPasswordRecoverAsync(username));

            Assert.Equal("Usuario no encontrado", exception.Message);
            A.CallTo(() => fakeCognitoClient.ForgotPasswordAsync(A<ForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ForgotPasswordRecoverAsync_GivenTooManyRequest_ThrowsTooManyRequestsException()
        {
            // Arrange
            var username = "testUsername";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ForgotPasswordAsync(A<ForgotPasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new TooManyRequestsException("Muchas solicitudes al mismo tiempo"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<TooManyRequestsException>(() => authProvider.ForgotPasswordRecoverAsync(username));

            Assert.Equal("Muchas solicitudes al mismo tiempo", exception.Message);
            A.CallTo(() => fakeCognitoClient.ForgotPasswordAsync(A<ForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ForgotPasswordRecoverAsync_TryTooManyAttempts_ThrowsLimitExceededException()
        {
            // Arrange
            var username = "testUsername";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "clientId";
            fakeConfiguration["ClientSecret"] = "clientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ForgotPasswordAsync(A<ForgotPasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new LimitExceededException("Muchos intentos"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<LimitExceededException>(() => authProvider.ForgotPasswordRecoverAsync(username));

            Assert.Equal("Muchos intentos", exception.Message);
            A.CallTo(() => fakeCognitoClient.ForgotPasswordAsync(A<ForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        // Recover Password Email
        [Fact]
        public async Task ConfirmForgotPasswordCodeAsync_GivenValidCode_ShouldChangeUserPassword()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "123456";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "fakeClientId";
            fakeConfiguration["UserPoolId"] = "fakeUserPoolId";
            fakeConfiguration["ClientSecret"] = "fakeClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            var confirmRequest = new ConfirmForgotPasswordRequest
            {
                ClientId = fakeConfiguration["ClientId"],
                Username = username,
                ConfirmationCode = confirmationCode,
                Password = password,
                SecretHash = authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)
            };

            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(confirmRequest, CancellationToken.None));

            var passwordRequest = new AdminSetUserPasswordRequest
            {
                Password = password,
                Username = username,
                Permanent = true,
                UserPoolId = fakeConfiguration["UserPoolId"],
            };
            A.CallTo(() => fakeCognitoClient.AdminSetUserPasswordAsync(passwordRequest, CancellationToken.None));

            // Act
            await authProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password);


            // Assert
            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.ConfirmationCode == confirmationCode &&
                req.Password == password &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeCognitoClient.AdminSetUserPasswordAsync(A<AdminSetUserPasswordRequest>.That.Matches(req =>
                req.Username == username &&
                req.Password == password &&
                req.UserPoolId == fakeConfiguration["UserPoolId"]), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmForgotPasswordCodeAsync_GivenExpiredCode_ThrowsExpiredCodeException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "testConfirmationCode";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "fakeClientId";
            fakeConfiguration["UserPoolId"] = "fakeUserPoolId";
            fakeConfiguration["ClientSecret"] = "fakeClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new ExpiredCodeException("Código expirado"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ExpiredCodeException>(() => authProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password));

            Assert.Equal("Código expirado", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.ConfirmationCode == confirmationCode &&
                req.Password == password &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmForgotPasswordCodeAsync_TryingTooManyTimes_ThrowsTooManyFailedAttemptsException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "testConfirmationCode";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "fakeClientId";
            fakeConfiguration["UserPoolId"] = "fakeUserPoolId";
            fakeConfiguration["ClientSecret"] = "fakeClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new TooManyFailedAttemptsException("Demasiados intentos fallidos"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<TooManyFailedAttemptsException>(() => authProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password));

            Assert.Equal("Demasiados intentos fallidos", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.ConfirmationCode == confirmationCode &&
                req.Password == password &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmForgotPasswordCodeAsync_GivenNotConfirmUser_ThrowsUserNotConfirmedException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "testConfirmationCode";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "fakeClientId";
            fakeConfiguration["UserPoolId"] = "fakeUserPoolId";
            fakeConfiguration["ClientSecret"] = "fakeClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotConfirmedException("Usuario no autenticado"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotConfirmedException>(() => authProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password));

            Assert.Equal("Usuario no autenticado", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.ConfirmationCode == confirmationCode &&
                req.Password == password &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmForgotPasswordCodeAsync_GivenNotRegisterUser_ThrowsUserNotFoundException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "testConfirmationCode";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "fakeClientId";
            fakeConfiguration["UserPoolId"] = "fakeUserPoolId";
            fakeConfiguration["ClientSecret"] = "fakeClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotFoundException("Usuario no encontrado"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(() => authProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password));

            Assert.Equal("Usuario no encontrado", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.ConfirmationCode == confirmationCode &&
                req.Password == password &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmForgotPasswordCodeAsync_GivenNotValidPassword_ThrowsInvalidPasswordException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "123456";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "fakeClientId";
            fakeConfiguration["UserPoolId"] = "fakeUserPoolId";
            fakeConfiguration["ClientSecret"] = "fakeClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            var confirmRequest = new ConfirmForgotPasswordRequest
            {
                ClientId = fakeConfiguration["ClientId"],
                Username = username,
                ConfirmationCode = confirmationCode,
                Password = password,
                SecretHash = authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)
            };

            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(confirmRequest, CancellationToken.None));

            A.CallTo(() => fakeCognitoClient.AdminSetUserPasswordAsync(A<AdminSetUserPasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new InvalidPasswordException("Contraseña inválida"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidPasswordException>(() => authProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password));

            Assert.Equal("Contraseña inválida", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.ConfirmationCode == confirmationCode &&
                req.Password == password &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeCognitoClient.AdminSetUserPasswordAsync(A<AdminSetUserPasswordRequest>.That.Matches(req =>
                req.Username == username &&
                req.Password == password &&
                req.UserPoolId == fakeConfiguration["UserPoolId"]), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ConfirmForgotPasswordCodeAsync_GivenNotRegisteredUser_ThrowsUserNotFoundException()
        {
            // Arrange
            var username = "testUser";
            var confirmationCode = "123456";
            var password = "testPassword";

            var fakeConfiguration = A.Fake<IConfiguration>();
            fakeConfiguration["ClientId"] = "fakeClientId";
            fakeConfiguration["UserPoolId"] = "fakeUserPoolId";
            fakeConfiguration["ClientSecret"] = "fakeClientSecret";

            var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

            var confirmRequest = new ConfirmForgotPasswordRequest
            {
                ClientId = fakeConfiguration["ClientId"],
                Username = username,
                ConfirmationCode = confirmationCode,
                Password = password,
                SecretHash = authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)
            };

            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(confirmRequest, CancellationToken.None));

            A.CallTo(() => fakeCognitoClient.AdminSetUserPasswordAsync(A<AdminSetUserPasswordRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotFoundException("Usuario no encontrado"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(() => authProvider.ConfirmForgotPasswordCodeAsync(username, confirmationCode, password));

            Assert.Equal("Usuario no encontrado", exception.Message);
            A.CallTo(() => fakeCognitoClient.ConfirmForgotPasswordAsync(A<ConfirmForgotPasswordRequest>.That.Matches(req =>
                req.ClientId == fakeConfiguration["ClientId"] &&
                req.Username == username &&
                req.ConfirmationCode == confirmationCode &&
                req.Password == password &&
                req.SecretHash == authProvider.CalculateSecretHash(fakeConfiguration["ClientId"], fakeConfiguration["ClientSecret"], username)), CancellationToken.None)).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeCognitoClient.AdminSetUserPasswordAsync(A<AdminSetUserPasswordRequest>.That.Matches(req =>
                req.Username == username &&
                req.Password == password &&
                req.UserPoolId == fakeConfiguration["UserPoolId"]), CancellationToken.None)).MustHaveHappenedOnceExactly();
        }

        //// Login Tests 

        //[Fact]
        //public async Task LoginUserAsync_GivenValidCredentials_ShouldLoginSuccessfully()
        //{
        //    // Arrange
        //    var username = "testUsername";
        //    var password = "testPassword";

        //    var fakeConfigurarion = A.Fake<IConfiguration>();
        //    fakeConfigurarion["ClientId"] = "testClientId";
        //    fakeConfigurarion["ClientSecret"] = "testClientSecret";
        //    var clientId = fakeConfigurarion["ClientId"];
        //    var clientSecret = fakeConfigurarion["ClientSecret"];

        //    var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
        //    var authProvider = new AuthProvider(fakeConfigurarion, fakeCognitoClient);

        //    var request = new InitiateAuthRequest
        //    {
        //        AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
        //        AuthParameters = new Dictionary<string, string>
        //        {
        //            { "USERNAME", username },
        //            { "PASSWORD", password },
        //            { "SECRET_HASH", authProvider.CalculateSecretHash(clientId, clientSecret, username) }
        //        },
        //        ClientId = clientId
        //    };
        //    A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(request, CancellationToken.None));

        //    // Act
        //    var response = await authProvider.LoginUserAsync(username, password);

        //    // Assert
        //    Assert.NotNull(response);
        //    A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.That.Matches(req =>
        //        req.AuthFlow == AuthFlowType.USER_PASSWORD_AUTH &&
        //        req.AuthParameters["USERNAME"] == username &&
        //        req.AuthParameters["PASSWORD"] == password &&
        //        req.ClientId == clientId &&
        //        req.AuthParameters["SECRET_HASH"] == authProvider.CalculateSecretHash(clientId, clientSecret, username)),
        //        CancellationToken.None)).MustHaveHappenedOnceExactly();
        //        }

        //[Fact]
        //public async Task LoginUserAsync_GivenNotConfirmUser_ThrowsUserNotConfirmedException()
        //{
        //    // Arrange
        //    var username = "testUsername";
        //    var password = "testPassword";

        //    var fakeConfiguration = A.Fake<IConfiguration>();
        //    fakeConfiguration["ClientId"] = "testClientId";
        //    fakeConfiguration["ClientSecret"] = "testClientSecret";
        //    var clientId = fakeConfiguration["ClientId"];
        //    var clientSecret = fakeConfiguration["ClientSecret"];

        //    var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
        //    var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

        //    A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotConfirmedException("El usuario no esta confirmado"));

        //    // Act & Assert
        //    var exception = await Assert.ThrowsAsync<UserNotConfirmedException>(() => authProvider.LoginUserAsync(username,password));

        //    Assert.Equal("El usuario no esta confirmado", exception.Message);
        //    A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.That.Matches(req =>
        //        req.AuthFlow == AuthFlowType.USER_PASSWORD_AUTH &&
        //        req.AuthParameters["USERNAME"] == username &&
        //        req.AuthParameters["PASSWORD"] == password &&
        //        req.ClientId == clientId &&
        //        req.AuthParameters["SECRET_HASH"] == authProvider.CalculateSecretHash(clientId, clientSecret, username)),
        //        CancellationToken.None)).MustHaveHappenedOnceExactly();
        //}

        //[Fact]
        //public async Task LoginUserAsync_GivenCaducatedPassword_ThrowsPasswordResetRequiredException()
        //{
        //    // Arrange
        //    var username = "testUsername";
        //    var password = "testPassword";

        //    var fakeConfiguration = A.Fake<IConfiguration>();
        //    fakeConfiguration["ClientId"] = "testClientId";
        //    fakeConfiguration["ClientSecret"] = "testClientSecret";
        //    var clientId = fakeConfiguration["ClientId"];
        //    var clientSecret = fakeConfiguration["ClientSecret"];

        //    var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
        //    var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

        //    A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new PasswordResetRequiredException("La contraseña se encuentra vencida"));

        //    // Act & Assert
        //    var exception = await Assert.ThrowsAsync<PasswordResetRequiredException>(() => authProvider.LoginUserAsync(username, password));

        //    Assert.Equal("La contraseña se encuentra vencida", exception.Message);
        //    A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.That.Matches(req =>
        //        req.AuthFlow == AuthFlowType.USER_PASSWORD_AUTH &&
        //        req.AuthParameters["USERNAME"] == username &&
        //        req.AuthParameters["PASSWORD"] == password &&
        //        req.ClientId == clientId &&
        //        req.AuthParameters["SECRET_HASH"] == authProvider.CalculateSecretHash(clientId, clientSecret, username)),
        //        CancellationToken.None)).MustHaveHappenedOnceExactly();
        //}

        //[Fact]
        //public async Task LoginUserAsync_GivenInvalidUsername_ThrowsUserNotFoundException()
        //{
        //    // Arrange
        //    var username = "testUsername";
        //    var password = "testPassword";

        //    var fakeConfiguration = A.Fake<IConfiguration>();
        //    fakeConfiguration["ClientId"] = "testClientId";
        //    fakeConfiguration["ClientSecret"] = "testClientSecret";
        //    var clientId = fakeConfiguration["ClientId"];
        //    var clientSecret = fakeConfiguration["ClientSecret"];

        //    var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
        //    var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

        //    A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.Ignored, CancellationToken.None)).ThrowsAsync(new UserNotFoundException("Usuario no encontrado"));

        //    // Act & Assert
        //    var exception = await Assert.ThrowsAsync<UserNotFoundException>(() => authProvider.LoginUserAsync(username, password));

        //    Assert.Equal("Usuario no encontrado", exception.Message);
        //    A.CallTo(() => fakeCognitoClient.InitiateAuthAsync(A<InitiateAuthRequest>.That.Matches(req =>
        //        req.AuthFlow == AuthFlowType.USER_PASSWORD_AUTH &&
        //        req.AuthParameters["USERNAME"] == username &&
        //        req.AuthParameters["PASSWORD"] == password &&
        //        req.ClientId == clientId &&
        //        req.AuthParameters["SECRET_HASH"] == authProvider.CalculateSecretHash(clientId, clientSecret, username)),
        //        CancellationToken.None)).MustHaveHappenedOnceExactly();
        //}
    }
}
