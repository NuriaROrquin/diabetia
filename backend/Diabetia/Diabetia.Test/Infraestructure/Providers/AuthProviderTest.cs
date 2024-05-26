using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Diabetia.Domain.Services;
using FakeItEasy;
using Infrastructure.Provider;
using Microsoft.Extensions.Configuration;

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

    }
}