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
        //[Fact]
        //public async Task RegisterUserAsync_WhenIConfirmedEmail_ReturnsUserSub()
        //{
        //    //preparar
        //    var region = Amazon.RegionEndpoint.USEast2;
        //    var service = new ApiCognitoProvider(region);
        //    var username = "testuser";
        //    var password = "testpassword";
        //    var email = "test@example.com";

        //    var userPoolId = "us-east-2_123";
        //    var clientId = "456";

        //    //mockeo
        //    var cognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
        //    var cognitoPool = A.Fake<CognitoUserPool>(options => options.Wrapping(new CognitoUserPool(userPoolId, clientId, cognitoClient)));



        //    A.CallTo(() => cognitoPool.SignUpAsync(A<string>._, A<string>._, A<Dictionary<string,string>>._, A<Dictionary<string, string>>._)).MustHaveHappened();
        //}

        [Fact]
        public async Task RegisterUserAsync_Calls_SignUpAsync()
        {
            // Arrange
            var region = Amazon.RegionEndpoint.USEast2;
            //var cognitoClient = A.Fake<AmazonCognitoIdentityProviderClient>();
            var cognitoIdentityProvider = A.Fake<IAmazonCognitoIdentityProvider>();
            string userPoolId = "us-east-2_jec9xbm7c";
            string  clientId= "7amuqfrhahhpgnfar29o1fk0u9";
            string clientSecret = "71mbrja4bf4oveoa2cgl7bhtpnjp5p2e6h7gtu99ubeiohskks3";
            var cognitoUserPool = A.Fake<CognitoUserPool>(options => options.WithArgumentsForConstructor(new object[] { userPoolId, clientId, cognitoIdentityProvider, clientSecret }));
            //clientConfig.RegionEndpoint = region;

            A.CallTo(() => cognitoUserPool.SignUpAsync(A<string>._, A<string>._, A<Dictionary<string, string>>._, A<Dictionary<string, string>>._)).MustHaveHappenedOnceExactly();

        //var apiCognitoProvider = new ApiCognitoProvider(cognitoClient, cognitoUserPool);

            //    // Act
            //    var result = await apiCognitoProvider.RegisterUserAsync("testUser", "testPassword", "test@example.com");

            //    // Assert
            //    Assert.Equal("success", result);
        }

    }
}
