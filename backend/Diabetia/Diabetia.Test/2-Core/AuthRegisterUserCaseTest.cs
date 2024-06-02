using Amazon.CognitoIdentityProvider.Model;
using Amazon.CognitoIdentityProvider;
using FakeItEasy;
using Infrastructure.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diabetia.Domain.Services;
using Diabetia.Domain.Repositories;
using System.Net;
using Diabetia.Application.UseCases;

namespace Diabetia.Test.Core
{
    public class AuthRegisterUserCaseTest
    {
        [Fact]
        public async Task RegisterUseCase_WhenCalledWithValidData_ShouldRegisterUserSuccessfully()
        {
            var username = "testUser";
            var password = "testPassword";
            var email = "test@user.com";
            var hashCode = "hashtest";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
      
            A.CallTo(() => fakeAuthProvider.RegisterUserAsync(username, password, email))
            .Returns(Task.FromResult(hashCode));
            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act
            await registerUseCase.Register(username, email, password);

            // Asserts
            A.CallTo(() => fakeAuthProvider.RegisterUserAsync(username, password, email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthRepository.SaveUserHashAsync(username, email, hashCode)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeAuthRepository.SaveUserUsernameAsync(email, username)).MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task RegisterUseCase_GivenEmailCode_ShouldConfirmUserSuccessfully()
        {
            var username = "testUser";
            var email = "test@user.com";
            var hashCode = "hashtest";
            var confirmationCode = "123456";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();

            A.CallTo(() => fakeAuthRepository.GetUserHashAsync(email)).Returns(Task.FromResult(hashCode));
            A.CallTo(() => fakeAuthProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode)).Returns(Task.FromResult(true));

            var registerUseCase = new AuthRegisterUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act
            await registerUseCase.ConfirmEmailVerification(username, email, confirmationCode);

            //// Asserts
            A.CallTo(() => fakeAuthProvider.ConfirmEmailVerificationAsync(username, hashCode, confirmationCode)).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeAuthRepository.GetUserHashAsync(email)).MustHaveHappenedOnceExactly();
        }
    }
}
