using NUnit.Framework;
using CarConnectApp.Entities;
using CarConnectApp.DAO;
using System;

namespace CarConnectAppTest
{
    [TestFixture]
    public class VehicleAddTest
    {
        private VehicleService _vehicleService;

        [SetUp]
        public void Setup()
        {
            _vehicleService = new VehicleService(); // Replace with your actual implementation
        }

        [Test]
        public void TestAddVehicle_Success()
        {
            // Arrange
            string make = "Toyota";                // Define make
            string model = "Corolla";              // Define model
            int year = 2022;                       // Define year (or any other valid year)
            DateTime manufactureDate = new DateTime(year, 1, 1);  // Set the full date (January 1st of the year)
            string color = "Blue";                 // Define color
            string regNo = "TN09AB1234";           // Define registration number
            bool available = true;                 // Define availability
            decimal rate = 1500.00M;               // Define daily rate

            Vehicle newVehicle = new Vehicle
            {
                VehicleID = 203,
                Make = make,
                Model = model,
                Year = manufactureDate,
                Color = color,
                RegistrationNumber = regNo,
                Availability = available,
                DailyRate = rate
            };

            // Act
            Vehicle addedVehicle = _vehicleService.AddVehicle(newVehicle);

            // Assert
            Assert.IsNotNull(addedVehicle, "Expected vehicle to be added and returned.");
            Assert.AreEqual("Toyota", addedVehicle.Make);
            Assert.AreEqual("TN09AB1234", addedVehicle.RegistrationNumber);
            Assert.IsTrue(addedVehicle.Availability);
        }
    }
}

