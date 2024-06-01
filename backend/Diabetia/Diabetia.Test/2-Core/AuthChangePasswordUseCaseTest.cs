using Diabetia.Application.UseCases;
using Diabetia.Domain.Repositories;
using Diabetia.Domain.Services;
using FakeItEasy;

namespace Diabetia.Test._2_Core
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
