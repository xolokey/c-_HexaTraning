using System;
using System.Collections.Generic;
using NUnit.Framework;
using CarConnectApp.DAO;
using CarConnectApp.Entities;
using Moq;

namespace CarConnectAppTest
{
    [TestFixture]
    public class AddVehicleTests
    {
        private Mock<IVehicleService<Vehicle>> _mockVehicleService;
        private List<Vehicle> _mockDatabase;

        [SetUp]
        public void Setup()
        {
            // Initialize a mock database (in-memory list)
            _mockDatabase = new List<Vehicle>();

            // Create a mock of IVehicleService
            _mockVehicleService = new Mock<IVehicleService<Vehicle>>();

            // Setup AddVehicle method to simulate adding a vehicle to the mock database
            _mockVehicleService.Setup(service => service.AddVehicle(It.IsAny<Vehicle>()))
                .Returns((Vehicle vehicle) =>
                {
                    // Check for duplicates
                    if (_mockDatabase.Exists(v => v.VehicleID == vehicle.VehicleID))
                    {
                        return null; // Simulate duplicate entry
                    }

                    _mockDatabase.Add(vehicle);
                    return vehicle;
                });
        }

        [Test]
        public void AddVehicle_ValidVehicle_ShouldAddVehicleSuccessfully()
        {
            // Arrange: Create a new vehicle object
            var newVehicle = new Vehicle
            {
                VehicleID = 1,
                Make = "Toyota",
                Model = "Camry",
                Year = 2020,
                Color = "Blue",
                RegistrationNumber = "XYZ1234",
                Availability = true,
                DailyRate = 50.0m
            };

            // Act: Add the vehicle using the mock service
            var addedVehicle = _mockVehicleService.Object.AddVehicle(newVehicle);

            // Assert: Check that the vehicle was added successfully
            Assert.That(addedVehicle, Is.Not.Null, "The vehicle should be added successfully.");
            Assert.That(addedVehicle.VehicleID, Is.EqualTo(newVehicle.VehicleID), "The added vehicle ID should match.");
        }

        [Test]
        public void AddVehicle_DuplicateVehicle_ShouldReturnNull()
        {
            // Arrange: Add a vehicle first
            var firstVehicle = new Vehicle
            {
                VehicleID = 1,
                Make = "Toyota",
                Model = "Camry",
                Year = 2020,
                Color = "Blue",
                RegistrationNumber = "XYZ1234",
                Availability = true,
                DailyRate = 50.0m
            };
            _mockVehicleService.Object.AddVehicle(firstVehicle);

            // Act: Try to add the same vehicle again
            var duplicateVehicle = new Vehicle
            {
                VehicleID = 1,
                Make = "Toyota",
                Model = "Camry",
                Year = 2020,
                Color = "Blue",
                RegistrationNumber = "XYZ1234",
                Availability = true,
                DailyRate = 50.0m
            };

            var result = _mockVehicleService.Object.AddVehicle(duplicateVehicle);

            // Assert: The result should be null if the vehicle is a duplicate
            Assert.That(result, Is.Null, "Duplicate vehicles should not be allowed.");
        }
    }
}
