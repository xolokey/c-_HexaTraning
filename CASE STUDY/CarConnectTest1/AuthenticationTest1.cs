using NUnit.Framework;
using CarConnectApp.Services;
using CarConnectApp.Entities;
using CarConnectApp.Exception;

namespace CarConnectAppTest
{
    [TestFixture]
    public class AuthenticationServiceTest
    {
        private AuthenticationService _authService;

        // 0. Setup method - initialize service before each test
        [SetUp]
        public void Setup()
        {
            _authService = new AuthenticationService();
        }

        // 1. Test customer authentication with invalid credentials

        // This test case verifies that an AuthenticationException is thrown 
        // when invalid credentials are used for a customer login.
        [Test]
        public void TestAuthenticateCustomer_InvalidCredentials_ThrowsAuthenticationException()
        {
            string invalidUsername = "fakeuser";
            string invalidPassword = "wrongpass";

            Assert.Throws<AuthenticationException>(() =>
                _authService.AuthenticateCustomer(invalidUsername, invalidPassword));
        }

        // 2. Test admin authentication with invalid credentials

        // This test case ensures an AuthenticationException is thrown for admin login
        // using incorrect credentials.
        [Test]
        public void TestAuthenticateAdmin_InvalidCredentials_ThrowsAuthenticationException()
        {
            string invalidUsername = "notadmin";
            string invalidPassword = "notapass";

            Assert.Throws<AuthenticationException>(() =>
                _authService.AuthenticateAdmin(invalidUsername, invalidPassword));
        }


    }
}
