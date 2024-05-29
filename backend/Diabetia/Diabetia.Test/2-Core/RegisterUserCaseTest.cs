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
    public class RegisterUserCaseTest
    {
        [Fact]
        public async Task RegisterUseCaseAsync_()
        {
            var username = "testUser";
            var password = "testPassword";
            var email = "test@user.com";
            var hashCode = "hashtest";

            var fakeAuthProvider = A.Fake<IAuthProvider>();
            var fakeAuthRepository = A.Fake<IAuthRepository>();
      
            A.CallTo(() => fakeAuthProvider.RegisterUserAsync(username, password, email))
            .Returns(Task.FromResult(hashCode));
            var registerUseCase = new RegisterUseCase(fakeAuthProvider, fakeAuthRepository);

            // Act
            await registerUseCase.Register(username, email, password);

            // Asserts
            A.CallTo(() => fakeAuthProvider.RegisterUserAsync(username, password, email)).MustHaveHappenedOnceExactly();

            A.CallTo(() => fakeAuthRepository.SaveUserHashAsync(username, email, hashCode)).MustHaveHappenedOnceExactly();
        }
    }
}
