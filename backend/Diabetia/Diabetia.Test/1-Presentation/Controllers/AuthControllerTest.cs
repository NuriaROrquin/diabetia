//using FakeItEasy;
//using Diabetia.Application.UseCases;
//using Microsoft.AspNetCore.Http;
//using Diabetia.API;
//using Diabetia.API.Controllers;

//namespace Diabetia.Test.Presentation.Controllers
//{
//    public class AuthControllerTest
//    {
//        [Fact]
//        public async Task Register_ShouldReturnOk_WhenUserIsRegisteredSuccessfully()
//        {
//            var fakeLoginUseCase = A.Fake<LoginUseCase>();
//            var fakeRegisterUseCase = A.Fake<RegisterUseCase>();
//            var fakeForgotPasswordUseCase = A.Fake<ForgotPasswordUseCase>();

//            var request = new RegisterRequest
//            {
//                userName = "testUser",
//                email = "test@user.com",
//                password = "testPassword"
//            };

//            A.CallTo(() => fakeRegisterUseCase.Register(request.userName, request.email, request.password))
//                .Returns(Task.CompletedTask);

//            var authController = new AuthController(fakeLoginUseCase, fakeRegisterUseCase, fakeForgotPasswordUseCase);

//            // Act
//            var result = await controller.Register(request);

//            // Assert
//            var okResult = Assert.IsType<OkObjectResult>(result);
//            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
//            Assert.Equal("Usuario registrado exitosamente", okResult.Value);

//            A.CallTo(() => fakeRegisterUseCase.Register(request.userName, request.email, request.password)).MustHaveHappenedOnceExactly();
//        }
//    }
        //[Fact]
        //public async Task ConfirmEmailAsync_GivenCodeSentToAccountEmail_ConfirmsEmail()
        //{
        //    // Arrange
        //    var username = "testUser";
        //    var password = "testPassword";
        //    var email = "test@user.com";
        //    var confirmationCode = "123456";
        //    var hashCode = "hashTest";

        //    var fakeConfiguration = A.Fake<IConfiguration>();
        //    fakeConfiguration["ClientId"] = "clientId";
        //    fakeConfiguration["ClientSecret"] = "clientSecret";

        //    var clientId = "clientId";
        //    var clientSecret = "clientSecret";

        //    var fakeCognitoClient = A.Fake<IAmazonCognitoIdentityProvider>();
        //    var authProvider = new AuthProvider(fakeConfiguration, fakeCognitoClient);

        //    var request = new ConfirmSignUpRequest
        //    {
        //        ClientId = fakeConfiguration["ClientId"],
        //        Username = username,
        //        ConfirmationCode = confirmationCode,
        //        SecretHash = hashCode
        //    };

        //    var response = new ConfirmSignUpResponse
        //    {
        //        HttpStatusCode = HttpStatusCode.OK
        //    };

        //    A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(
        //        A<ConfirmSignUpRequest>.That.Matches(req =>
        //            req.ClientId == fakeConfiguration["ClientId"] &&
        //            req.Username == username &&
        //            req.ConfirmationCode == confirmationCode &&
        //            req.SecretHash == hashCode),
        //        CancellationToken.None))
        //        .Returns(Task.FromResult(response));

        //    // Act
        //    var result = await authProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode);

        //    // Assert
        //    Assert.True(result);
        //    Assert.NotNull(result);
        //    A.CallTo(() => fakeCognitoClient.ConfirmSignUpAsync(
        //        A<ConfirmSignUpRequest>.That.Matches(req =>
        //            req.ClientId == fakeConfiguration["ClientId"] &&
        //            req.Username == username &&
        //            req.ConfirmationCode == confirmationCode &&
        //            req.SecretHash == hashCode), CancellationToken.None)).MustHaveHappenedOnceExactly();
        //}
//    }
//}
