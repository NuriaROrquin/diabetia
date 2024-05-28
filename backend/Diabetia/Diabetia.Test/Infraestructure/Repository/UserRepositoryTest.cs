using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using FakeItEasy;
using System.Data.Entity;

namespace Diabetia.Test.Infraestructure.Repository
{
    public class UserRepositoryTest
    {

        [Fact]

        public async Task CompleteUserInfo_UserExist_ShouldUpdateUserInfo()
        {

            var name = "Test";
            var lastname = "User";
            var email = "test@user.com";
            var gender = "M";
            var weight = "70";
            var phone = "1122334455";

            var fakeContext = A.Fake<diabetiaContext>();
            var fakeDbSet = A.Fake<System.Data.Entity.DbSet<Usuario>>();
            var fakeUser = new Usuario
            {
                Email = "test@user.com",
                NombreCompleto = "John Doe",
                Genero = "Male",
                Telefono = "123456789"
            };

            //A.CallTo(() => fakeContext.Set<Usuario>()).Returns(fakeDbSet)

            A.CallTo(() => fakeDbSet.FirstOrDefaultAsync(u => u.Email == fakeUser.Email))
            .Returns(Task.FromResult(fakeUser));

            var userRepository = new UserRepository(fakeContext); // Inject fake DbContext into service

            // Act
            await userRepository.CompleteUserInfo("Jane", "test@example.com", "Female", "Doe", 60, "987654321");

            // Assert
            // Ensure that the user's information is updated as expected
            Assert.Equal("Jane Doe", fakeUser.NombreCompleto);
            Assert.Equal("Female", fakeUser.Genero);
            Assert.Equal("987654321", fakeUser.Telefono);

            // Verify that Add and SaveChangesAsync were not called because the user already exists
            A.CallTo(() => fakeDbSet.Add(A<Usuario>._)).MustNotHaveHappened();
            //A.CallTo(() => fakeContext.SaveChangesAsync()).MustNotHaveHappened();


        }


    }
}
