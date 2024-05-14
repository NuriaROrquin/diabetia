using System;
using Xunit;
using Infraestructura.Provider;
using Amazon.CognitoIdentityProvider.Model;
using System.Net;
using Amazon.CognitoIdentityProvider;
using FakeItEasy;
using Amazon.Extensions.CognitoAuthentication;

namespace Diabetia.Test.Infrastructure.ProvidersTest
{
    public class CognitoTests
    {
        [Fact]
        public async Task RegisterUserAsync_WhenIConfirmedEmail_ReturnsUserSub()
        {
            //preparar
            var region = Amazon.RegionEndpoint.USEast2;
            var service = new ApiCognitoService(region);
            var username = "testuser";
            var password = "testpassword";
            var email = "test@example.com";

            var userPoolId = "us-east-2_123";
            var clientId = "456";

            //mockeo
            var cognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
            var cognitoPool = A.Fake<CognitoUserPool>(options => options.Wrapping(new CognitoUserPool(userPoolId, clientId, cognitoClient)));



            A.CallTo(() => cognitoPool.SignUpAsync(A<string>._, A<string>._, A<Dictionary<string,string>>._, A<Dictionary<string, string>>._)).MustHaveHappened();
        }

    }
}
