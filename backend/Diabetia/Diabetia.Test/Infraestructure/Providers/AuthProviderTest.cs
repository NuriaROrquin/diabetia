using Amazon.Extensions.CognitoAuthentication;
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
            // Arrange
            //var username = "testUser";
            //var password = "testPassword";
            //var email = "test@example.com";
            //var clientId = "yourClientId";
            //var clientSecret = "yourClientSecret";
            //var userAttributes = new Dictionary<string, string> {
            //        { "email", email }
            //    };
            //var fakeConfiguration = A.Fake<IConfiguration>();
            //fakeConfiguration["Region"] = "yourRegion";
            //fakeConfiguration["UserPoolId"] = "yourUserPoolId";
            //fakeConfiguration["awsAccessKey"] = "yourAwsAccessKey";
            //fakeConfiguration["awsSecretKey"] = "yourAwsSecretKey";
            //fakeConfiguration["ClientId"] = "clientId";
            //fakeConfiguration["ClientSecret"] = "clientSecret";

            ////var fakeAuthProvider = new AuthProvider(fakeConfiguration);
            //var fakeCognitoUserPool = A.Fake<CognitoUserPool>();

            //A.CallTo(() => fakeCognitoUserPool.SignUpAsync(username, password, userAttributes, null)).Returns(Task.FromResult("calculatedSecretHash"));
            //var result = await fakeAuthProvider.RegisterUserAsync(username, password, email);

            //Assert.NotNull(result);
            //Assert.Equal("calculatedSecretHash", result);
        }

    }
}