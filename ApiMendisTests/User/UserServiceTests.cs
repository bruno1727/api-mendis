using ApiMendis.Notifications;
using Moq;
using Xunit;

namespace ApiMendis.User.Tests
{
    public class UserServiceTests
    {

        [Fact]
        public async Task SignUp_CalledInsertAsync()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var notificationMock = new Mock<INotificationService>();
            var user = new User()
            {
                Email = "teste@email.com"
            };

            repositoryMock.Setup((mock) => mock.InsertAsync(It.IsAny<User>())).ReturnsAsync(true);

            var service = new UserService(notificationMock.Object, repositoryMock.Object);
            await service.SignUpAsync(user);

            repositoryMock.Verify(mock => mock.InsertAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public async Task SignUp_ValidEmailAsync()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var notificationMock = new Mock<INotificationService>();
            var user = new User()
            {
                Email = "teste@email.com"
            };

            repositoryMock.Setup((mock) => mock.InsertAsync(It.IsAny<User>())).ReturnsAsync(true);

            var service = new UserService(notificationMock.Object, repositoryMock.Object);

            var result = await service.SignUpAsync(user);

            Assert.True(result);
        }

        [Fact]
        public async Task SignUp_InvalidEmailAsync()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var notificationMock = new Mock<INotificationService>();
            var user = new User()
            {
                Email = "teste.com"
            };

            var service = new UserService(notificationMock.Object, repositoryMock.Object);
            var result = await service.SignUpAsync(user);

            Assert.False(result);
            notificationMock.Verify(mock => mock.Add(ValidationMessages.InvalidEmail), Times.Once());
        }

        [Fact]
        public async Task SignUp_NotInserted_ReturnsFalseAsync()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var notificationMock = new Mock<INotificationService>();
            var user = new User()
            {
                Email = "teste@email.com"
            };

            var service = new UserService(notificationMock.Object, repositoryMock.Object);
            repositoryMock.Setup((mock) => mock.InsertAsync(It.IsAny<User>())).ReturnsAsync(false);
            var result = await service.SignUpAsync(user);

            Assert.False(result);
        }
        
        [Fact]
        public async Task SignUp_Inserted_ReturnsTrueAsync()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var notificationMock = new Mock<INotificationService>();
            var user = new User()
            {
                Email = "teste@email.com"
            };

            var service = new UserService(notificationMock.Object, repositoryMock.Object);
            repositoryMock.Setup((mock) => mock.InsertAsync(It.IsAny<User>())).ReturnsAsync(true);
            var result = await service.SignUpAsync(user);

            Assert.True(result);
        }
    }
}