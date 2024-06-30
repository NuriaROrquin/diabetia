using Diabetia.Domain.Entities;
using Diabetia.Domain.Exceptions;
using Diabetia.Domain.Models;
using Diabetia.Infrastructure.EF;
using Diabetia.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;

namespace Diabetia_Infrastructure.Repositories.Auth
{
    public class AuthTest
    {
        // ---------------------------------------------- ⬇⬇ Get Test ⬇⬇ -------------------------------------------
        [Fact]
        public async Task GetUsernameByEmailAsync_GivenEmail_ShouldReturnsUsernameSuccessfully()
        {
            //Arrange
            string email = "fakeEmail@gmail.com";
            string username = "test";
            var mockContext = CreateMockContextGetUserInformation();
            var repository = new AuthRepository(mockContext.Object);

            //Act
            var response = await repository.GetUsernameByEmailAsync(email);

            //Assert
            Assert.Equal(response, username);
        }

        [Fact]
        public async Task GetUsernameByEmailAsync_GivenInvalidEmail_ShouldReturnsUsernameEmpty()
        {
            //Arrange
            string email = "nonExist@gmail.com";
            string username = "";
            var mockContext = CreateMockContextGetUserInformation();
            var repository = new AuthRepository(mockContext.Object);

            //Act
            var response = await repository.GetUsernameByEmailAsync(email);

            //Assert
            Assert.Equal(response, username);
        }

        [Fact]
        public async Task GetUserHashAsync_GivenEmail_ShouldReturnsHashSuccessfully()
        {
            //Arrange
            string email = "fakeEmail@gmail.com";
            string hash = "HashTest";
            var mockContext = CreateMockContextGetUserInformation();
            var repository = new AuthRepository(mockContext.Object);

            //Act
            var response = await repository.GetUserHashAsync(email);

            //Assert
            Assert.Equal(response, hash);
        }

        [Fact]
        public async Task GetUserHashAsync_GivenInvalidEmail_ShouldReturnsHashEmpty()
        {
            //Arrange
            string email = "noneExist@gmail.com";
            string hash = "";
            var mockContext = CreateMockContextGetUserInformation();
            var repository = new AuthRepository(mockContext.Object);

            //Act
            var response = await repository.GetUserHashAsync(email);

            //Assert
            Assert.Equal(response, hash);
        }

        [Fact]
        public async Task GetUserStateAsync_GivenEmail_ShouldReturnsUserStateSuccessfully()
        {
            //Arrange
            string email = "fakeEmail@gmail.com";
            bool state = true;
            var mockContext = CreateMockContextGetUserInformation();
            var repository = new AuthRepository(mockContext.Object);

            //Act
            var response = await repository.GetUserStateAsync(email);

            //Assert
            Assert.True(response);
        }

        [Fact]
        public async Task GetUserStateAsync_GivenInvalidEmail_ThrowsNotImplementedException()
        {
            //Arrange
            string email = "fakeEmail@gmail.com";
            var mockContext = CreateMockContextWithNoUsers();
            var repository = new AuthRepository(mockContext.Object);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotImplementedException>(
                    () => repository.GetUserStateAsync(email));
        }

        [Fact]
        public async Task GetUserStateAsync_GivenValidEmailInvalidState_ThrowsInvalidOperationException()
        {
            //Arrange
            string email = "fakeEmail@gmail.com";
            var mockContext = CreateMockContextGetUserStateAsync();
            var repository = new AuthRepository(mockContext.Object);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                    () => repository.GetUserStateAsync(email));
        }

        private Mock<diabetiaContext> CreateMockContextGetUserStateAsync()
        {
            var user = new Usuario() { Id = 1, Email = "fakeEmail@gmail.com" };
            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            return mockContext;
        }

        // ---------------------------------------------- ⬇⬇ Save Test ⬇⬇ -------------------------------------------

        [Fact]
        public async Task SaveUserHashAsync_GivenNewUser_ShouldAddNewUser()
        {
            // Arrange
            var mockContext = CreateMockContextSaveNewUserHashAsync();
            var repository = new AuthRepository(mockContext.Object);

            var newUser = new Usuario { Email = "newuser@test.com", Username = "NewUser" };
            var hash = "newHash";

            // Act
            await repository.SaveUserHashAsync(newUser, hash);

            // Assert
            mockContext.Verify(m => m.Usuarios.Add(It.Is<Usuario>(u => u.Email == newUser.Email && u.Hash == hash)), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private Mock<diabetiaContext> CreateMockContextSaveNewUserHashAsync()
        {
            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario>());

            return mockContext;
        }


        [Fact]
        public async Task SaveUserHashAsync_GivenExistingUser_ShouldUpdateHash()
        {
            // Arrange
            var mockContext = CreateMockContextSaveUserHashAsync();
            var repository = new AuthRepository(mockContext.Object);

            var existingUser = new Usuario { Email = "test@gmail.com", Username = "ExistingUser" };
            var newHash = "newHash";

            // Act
            await repository.SaveUserHashAsync(existingUser, newHash);

            // Assert
            var user = mockContext.Object.Usuarios.FirstOrDefault(u => u.Email == existingUser.Email);
            Assert.Equal(newHash, user.Hash);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        }

        private Mock<diabetiaContext> CreateMockContextSaveUserHashAsync()
        {
            var user = new Usuario() { Id = 1, Email = "test@gmail.com", Hash = "TestHash" };

            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });

            return mockContext;
        }

        [Fact]
        public async Task SaveUserUsernameAsync_GivenUser_ShouldUpdateUsernameSuccessfully()
        {
            // Arrange
            var mockContext = CreateMockContextSaveUsernameAsync();
            var repository = new AuthRepository(mockContext.Object);
            var user = new Usuario() { Id = 1, Email = "test@gmail.com", Username = "Test" };

            // Act
            await repository.SaveUserUsernameAsync(user);

            // Assert
            var userCheck = mockContext.Object.Usuarios.FirstOrDefault(u => u.Email == user.Email);
            Assert.NotNull(userCheck);
            Assert.Equal(user.Username.ToLower(), userCheck.Username);
        }

        private Mock<diabetiaContext> CreateMockContextSaveUsernameAsync()
        {
            var user = new Usuario() { Id = 1, Email = "test@gmail.com", Hash = "TestHash", Username = "Test" };

            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });

            return mockContext;
        }

        [Fact]
        public async Task SaveUserUsernameAsync_GivenNonExistingUser_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var mockContext = CreateMockContextWithNoUsers();
            var repository = new AuthRepository(mockContext.Object);

            var nonExistingUser = new Usuario { Email = "test@gmail.com", Username = "Testing" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => repository.SaveUserUsernameAsync(nonExistingUser)
            );

            Assert.Equal($"No se encontró un usuario con el email {nonExistingUser.Email}.", exception.Message);
        }

        [Fact]
        public async Task SetUserStateActiveAsync_GivenValidEmail_ShouldUpdateUserStateSuccessfully()
        {
            // Arrange
            var email = "test@gmail.com";
            var mockContext = CreateMockContextUpdateState();
            var repository = new AuthRepository(mockContext.Object);

            // Act
            await repository.SetUserStateActiveAsync(email);

            //Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
            var updatedUser = mockContext.Object.Usuarios.FirstOrDefault(u => u.Email == email);
            Assert.True(updatedUser.EstaActivo);
        }

        private Mock<diabetiaContext> CreateMockContextUpdateState()
        {
            var user = new Usuario() { Id = 1, Email = "test@gmail.com", EstaActivo = false };
            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            return mockContext;
        }

        [Fact]
        public async Task SetUserStateActiveAsync_GivenInvalidUser_ThrowsKeyNotFoundException()
        {
            // Arrange
            var mockContext = CreateMockContextWithNoUsers();
            var repository = new AuthRepository(mockContext.Object);

            var user = new Usuario { Email = "test@gmail.com", Username = "Testing" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => repository.SetUserStateActiveAsync(user.Email)
            );

            Assert.Equal($"No se encontró un usuario con el email {user.Email}.", exception.Message);
        }

        // ---------------------------------------------- ⬇⬇ Check Test ⬇⬇ -------------------------------------------
        [Fact]
        public async Task CheckUsernameOnDatabaseAsync_GivenValidUsername_ShouldReturnsTrue()
        {
            // Arrange
            string username = "Test";
            var mockContext = CreateMockContextGetUserInformation();
            var repository = new AuthRepository(mockContext.Object);

            // Act
            bool response = await repository.CheckUsernameOnDatabaseAsync(username);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task CheckUsernameOnDatabaseAsync_GivenInvalidUsername_ShouldReturnsFalse()
        {
            // Arrange
            string username = "Test";
            var mockContext = CreateMockContextWithNoUsers();
            var repository = new AuthRepository(mockContext.Object);

            // Act
            bool response = await repository.CheckUsernameOnDatabaseAsync(username);

            // Assert
            Assert.False(response);
        }

        [Fact]
        public async Task CheckEmailOnDatabaseAsync_GivenInvalidEmail_ThrowsEmailAlreadyExistsException()
        {
            //Assert
            var email = "fakeEmail@gmail.com";
            var mockContext = CreateMockContextGetUserInformation();
            var repository = new AuthRepository(mockContext.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EmailAlreadyExistsException>(
                () => repository.CheckEmailOnDatabaseAsync(email)
            );

        }

        [Fact]
        public async Task CheckEmailOnDatabaseAsync_GivenNonExistingEmail_ShouldNotThrowException()
        {
            // Arrange
            var email = "noExist@gmail.com";
            var mockContext = CreateMockContextGetUserInformation();
            var repository = new AuthRepository(mockContext.Object);

            // Act & Assert
            var exception = await Record.ExceptionAsync(() => repository.CheckEmailOnDatabaseAsync(email));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task ResetUserAttemptsAsync_GivenValidUsername_ShouldResetAttemptsSuccessfully()
        {
            //Arrange
            var username = "test";
            var mockContext = CreateMockContextGetUserInformation();
            var repository = new AuthRepository(mockContext.Object);

            //Act
            await repository.ResetUserAttemptsAsync(username);

            //Assert
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
            var updatedUser = mockContext.Object.Usuarios.FirstOrDefault(u => u.Username == username);
            Assert.Equal(updatedUser.IntentosFallidos, 0);
        }

        [Fact]
        public async Task ResetUserAttemptsAsync_GivenInvalidUsername_ThrowsKeyNotFoundException()
        {
            // Arrange
            var mockContext = CreateMockContextWithNoUsers();
            var repository = new AuthRepository(mockContext.Object);

            var user = new Usuario { Email = "test@gmail.com", Username = "Testing" };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
                () => repository.ResetUserAttemptsAsync(user.Username)
            );

            Assert.Equal($"No se encontró un usuario con el nombre {user.Username}.", exception.Message);
        }

        // -------------------------------------------- ⬇⬇ General Contexts ⬇⬇ -----------------------------------------

        private Mock<diabetiaContext> CreateMockContextGetUserInformation()
        {
            var user = new Usuario() { Id = 1, Email = "fakeEmail@gmail.com", Username = "test", EstaActivo = true, Hash = "HashTest", IntentosFallidos = 1 };

            var mockContext = new Mock<diabetiaContext>();

            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario> { user });
            return mockContext;
        }

        private Mock<diabetiaContext> CreateMockContextWithNoUsers()
        {
            var mockContext = new Mock<diabetiaContext>();
            mockContext.Setup(m => m.Usuarios).ReturnsDbSet(new List<Usuario>());

            return mockContext;
        }
    }
}
