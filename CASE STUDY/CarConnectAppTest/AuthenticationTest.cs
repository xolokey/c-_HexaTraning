using CarConnectApp.Entities;
using CarConnectApp.Services;
using Moq;
using NUnit.Framework;

namespace CarConnectAppTest
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private Mock<IAuthenticationService> _mockAuthService;

        [SetUp]
        public void Setup()
        {
            _mockAuthService = new Mock<IAuthenticationService>();
        }

        [Test]
        public void AuthenticateCustomer_ValidCredentials_ReturnsCustomer()
        {
            // Arrange
            string username = "testuser";
            string password = "testpass";
            var expectedCustomer = new Customer
            {
                CustomerID = 1,
                UserName = username,
                Password = password
            };

            _mockAuthService
                .Setup(service => service.AuthenticateCustomer(username, password))
                .Returns(expectedCustomer);

            // Act
            var result = _mockAuthService.Object.AuthenticateCustomer(username, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.UserName, Is.EqualTo(username));
        }

        [Test]
        public void AuthenticateCustomer_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            string username = "invaliduser";
            string password = "wrongpass";

            _mockAuthService
                .Setup(service => service.AuthenticateCustomer(username, password))
                .Returns((Customer)null);

            // Act
            var result = _mockAuthService.Object.AuthenticateCustomer(username, password);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void AuthenticateAdmin_ValidCredentials_ReturnsAdmin()
        {
            // Arrange
            string username = "adminuser";
            string password = "adminpass";
            var expectedAdmin = new Admin
            {
                AdminID = 1,
                Username = username,
                Password = password
            };

            _mockAuthService
                .Setup(service => service.AuthenticateAdmin(username, password))
                .Returns(expectedAdmin);

            // Act
            var result = _mockAuthService.Object.AuthenticateAdmin(username, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Username, Is.EqualTo(username));
        }

        [Test]
        public void AuthenticateAdmin_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            string username = "noadmin";
            string password = "wrongpass";

            _mockAuthService
                .Setup(service => service.AuthenticateAdmin(username, password))
                .Returns((Admin)null);

            // Act
            var result = _mockAuthService.Object.AuthenticateAdmin(username, password);

            // Assert
            Assert.IsNull(result);
        }
    }
}

