using NUnit.Framework;
using CarConnectApp.Entities;
using CarConnectApp.DAO;
using System;

namespace CarConnectAppTest
{
    [TestFixture]
    public class CustomerConsoleStyleUpdateTest
    {
        private ICustomerService<Customer> _customerService;

        [SetUp]
        public void Setup()
        {
            _customerService = new CustomerService(); // Replace with actual implementation
        }
        [Test]
        public void TestGetCustomer_Only()
        {
            var customer = _customerService.GetCustomerById(101);
            Assert.IsNotNull(customer, "Customer with ID 101 not found");
            Console.WriteLine($"Customer: {customer.FirstName}, {customer.UserName}");
        }

        [Test]
        public void TestUpdateCustomer_ID_101_Success()
        {
            // Arrange: Fetch existing customer
            int updateCustomerId = 101;
            Customer customerToUpdate = _customerService.GetCustomerById(updateCustomerId);

            Assert.IsNotNull(customerToUpdate, "Customer with ID 101 should exist.");

            // Simulating console-style field updates
            customerToUpdate.FirstName = "UpdatedName";
            customerToUpdate.LastName = "LastUpdated";
            customerToUpdate.Email = "updatedemail@example.com";
            customerToUpdate.PhoneNumber = "1234567890";
            customerToUpdate.Address = "Updated Address";
            customerToUpdate.UserName = "customer1";
            customerToUpdate.Password = "newpass123";

            // Act: Perform the update
            Customer updatedCustomer = _customerService.UpdateCustomer(customerToUpdate);

            // Assert
            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual("UpdatedName", updatedCustomer.FirstName);
            Assert.AreEqual("LastUpdated", updatedCustomer.LastName);
            Assert.AreEqual("updatedemail@example.com", updatedCustomer.Email);
            Assert.AreEqual("1234567890", updatedCustomer.PhoneNumber);
            Assert.AreEqual("Updated Address", updatedCustomer.Address);
            Assert.AreEqual("customer1", updatedCustomer.UserName);
            Assert.AreEqual("newpass123", updatedCustomer.Password);
        }
    }
}
