using System;
using System.Collections.Generic;
using CarConnectApp.Entities;
using CarConnectApp.DAO;
using Moq;
using NUnit.Framework;

namespace CarConnectAppTest
{
    [TestFixture]
    public class AvailableVehicleTests
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

            // Setup GetAvailableVehicles to return only available vehicles
            _mockVehicleService.Setup(service => service.GetAvailableVehicles())
                .Returns(() => _mockDatabase.FindAll(v => v.Availability));

            // Setup AddVehicle to add vehicles to the mock database
            _mockVehicleService.Setup(service => service.AddVehicle(It.IsAny<Vehicle>()))
                .Callback<Vehicle>(vehicle => _mockDatabase.Add(vehicle));
        }

        [Test]
        public void GetAvailableVehicles_ShouldReturnOnlyAvailableVehicles()
        {
            // Arrange: Add vehicles to the mock database
            var vehicle1 = new Vehicle
            {
                VehicleID = 1,
                Make = "Toyota",
                Model = "Camry",
                Year = 2020,
                RegistrationNumber = "XYZ1234",
                DailyRate = 50.0m,
                Availability = true
            };
            var vehicle2 = new Vehicle
            {
                VehicleID = 2,
                Make = "Honda",
                Model = "Civic",
                Year = 2021,
                RegistrationNumber = "ABC5678",
                DailyRate = 60.0m,
                Availability = false // Not available
            };
            var vehicle3 = new Vehicle
            {
                VehicleID = 3,
                Make = "Ford",
                Model = "Focus",
                Year = 2022,
                RegistrationNumber = "DEF9012",
                DailyRate = 45.0m,
                Availability = true
            };
            _mockVehicleService.Object.AddVehicle(vehicle1);
            _mockVehicleService.Object.AddVehicle(vehicle2);
            _mockVehicleService.Object.AddVehicle(vehicle3);

            // Act: Get the list of available vehicles
            List<Vehicle> availableVehicles = _mockVehicleService.Object.GetAvailableVehicles();

            // Assert: Ensure the list only contains available vehicles
            Assert.That(availableVehicles.Count, Is.EqualTo(2), "There should be 2 available vehicles.");
            Assert.That(availableVehicles.Exists(v => v.VehicleID == 1), Is.True, "Vehicle with ID 1 should be available.");
            Assert.That(availableVehicles.Exists(v => v.VehicleID == 3), Is.True, "Vehicle with ID 3 should be available.");
            Assert.That(availableVehicles.Exists(v => v.VehicleID == 2), Is.False, "Vehicle with ID 2 should not be in the list as it is unavailable.");
        }

        [Test]
        public void GetAvailableVehicles_WhenNoAvailableVehicles_ShouldReturnEmptyList()
        {
            // Arrange: Add vehicles but set them all as unavailable
            var vehicle1 = new Vehicle
            {
                VehicleID = 1,
                Make = "Toyota",
                Model = "Camry",
                Year = 2020,
                RegistrationNumber = "XYZ1234",
                DailyRate = 50.0m,
                Availability = false
            };
            var vehicle2 = new Vehicle
            {
                VehicleID = 2,
                Make = "Honda",
                Model = "Civic",
                Year = 2021,
                RegistrationNumber = "ABC5678",
                DailyRate = 60.0m,
                Availability = false
            };
            _mockVehicleService.Object.AddVehicle(vehicle1);
            _mockVehicleService.Object.AddVehicle(vehicle2);

            // Act: Get the list of available vehicles
            List<Vehicle> availableVehicles = _mockVehicleService.Object.GetAvailableVehicles();

            // Assert: The list should be empty if no vehicles are available
            Assert.That(availableVehicles.Count, Is.EqualTo(0), "There should be no available vehicles.");
        }
    }
}
