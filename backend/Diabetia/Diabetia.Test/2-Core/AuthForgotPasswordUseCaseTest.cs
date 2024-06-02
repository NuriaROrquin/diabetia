using Diabetia.Application.UseCases;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using FakeItEasy;

namespace Diabetia.Test._2_Core
{
    public class AuthForgotPasswordUseCaseTest
    {
        [Fact]
        public async Task ForgotPasswordUseCase_WhenCalledWithValidData_ShouldSendEmailSuccessfully()
        {
            // Arrange
            var email = "testEmail@gmail.com";
            var username = "testUsername";
            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            A.CallTo(() => fakeAuthRepository.GetUsernameByEmail(email)).Returns(username);
            A.CallTo(() => fakeAuthProvider.ForgotPasswordRecoverAsync(username));

            var forgotPasswordUseCase = new AuthForgotPasswordUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act
            await forgotPasswordUseCase.ForgotPasswordEmailAsync(email);

            // Asserts
            A.CallTo(() => fakeAuthProvider.ForgotPasswordRecoverAsync(username)).MustHaveHappenedOnceExactly();
        }
        
    }
}
