using Diabetia.Application.UseCases.AuthUseCases;
using Diabetia.Domain.Services;
using FakeItEasy;

namespace Diabetia_Core.Auth
{
    public class AuthChangePasswordUseCaseTest
    {
        [Fact]
        public async Task ChangePasswordUseCase_WhenCalledWithValidData_ShouldChangePasswordSuccessfully()
        {
            var accessToken = "tokenTest";
            var previousPassword = "testPreviousPassword";
            var newPassword = "testNewPassword";
            var fakeAuthProvider = A.Fake<IAuthProvider>();

            A.CallTo(() => fakeAuthProvider.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword));

            var changePasswordUseCase = new AuthChangePasswordUseCase(fakeAuthProvider);

            // Act
            await changePasswordUseCase.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword);

            // Asserts
            A.CallTo(() => fakeAuthProvider.ChangeUserPasswordAsync(accessToken, previousPassword, newPassword)).MustHaveHappenedOnceExactly();

        }

    }
}
