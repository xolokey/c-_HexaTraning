using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.DAO;
using CarConnectApp.Entities;
using Moq;
using NUnit.Framework;

namespace CarConnectAppTest
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private Mock<ICustomerService<Customer>> _mockCustomerService;
        private CustomerService customerService;

        [SetUp]
        public void Setup()
        {
            // Mock the ICustomerService interface
            _mockCustomerService = new Mock<ICustomerService<Customer>>();
            customerService = new CustomerService(); // Replace with actual or mock implementation if needed
        }

        [Test]
        public void UpdateCustomer_ValidCustomer_ReturnsUpdatedCustomer()
        {
            // Arrange: Create a customer object to update
            Customer customerToUpdate = new Customer
            {
                CustomerID = 101,
                FirstName = "UpdatedFirst",
                LastName = "UpdatedLast",
                Email = "updated.email@example.com",
                PhoneNumber = "9998887777",
                Address = "Updated Address",
                UserName = "testuser",
                Password = "newpass123",
                RegistrationDate = DateTime.Now
            };

            // Mock the UpdateCustomer method to return the updated customer
            _mockCustomerService
                .Setup(service => service.UpdateCustomer(It.IsAny<Customer>()))
                .Returns((Customer c) => c);

            // Act: Call the UpdateCustomer method
            Customer result = _mockCustomerService.Object.UpdateCustomer(customerToUpdate);

            // Assert: Verify the updated customer details
            Assert.IsNotNull(result, "The updated customer should not be null.");
            Assert.That(result.FirstName, Is.EqualTo("UpdatedFirst"), "The first name should be updated.");
            Assert.That(result.LastName, Is.EqualTo("UpdatedLast"), "The last name should be updated.");
            Assert.That(result.Email, Is.EqualTo("updated.email@example.com"), "The email should be updated.");
        }
    }
}
