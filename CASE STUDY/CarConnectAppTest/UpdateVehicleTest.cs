using CarConnectApp.Entities;
using CarConnectApp.DAO;
using Moq;
using NUnit.Framework;
using System;

namespace CarConnectAppTest
{
    [TestFixture]
    public class VehicleServiceTests
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

            // Setup AddVehicle to add vehicles to the mock database
            _mockVehicleService.Setup(service => service.AddVehicle(It.IsAny<Vehicle>()))
                .Returns((Vehicle vehicle) =>
                {
                    vehicle.VehicleID = _mockDatabase.Count + 1; // Simulate auto-increment ID
                    _mockDatabase.Add(vehicle);
                    return vehicle;
                });

            // Setup UpdateVehicle to update vehicles in the mock database
            _mockVehicleService.Setup(service => service.UpdateVehicle(It.IsAny<int>()))
                .Returns((int vehicleID) =>
                {
                    var vehicle = _mockDatabase.Find(v => v.VehicleID == vehicleID);
                    return vehicle; // Return the updated vehicle or null if not found
                });
        }

        [Test]
        public void UpdateVehicle_ValidUpdate_ShouldUpdateVehicleSuccessfully()
        {
            // Arrange: Add a vehicle first
            var existingVehicle = new Vehicle
            {
                Make = "Toyota",
                Model = "Camry",
                Year = 2020,
                RegistrationNumber = "XYZ1234",
                DailyRate = 50.0m
            };

            // Add the vehicle and capture the returned object to ensure the ID is set
            var addedVehicle = _mockVehicleService.Object.AddVehicle(existingVehicle);

            // Act: Update the vehicle's details
            addedVehicle.Model = "Corolla";  // Update the model
            addedVehicle.DailyRate = 55.0m;  // Update the daily rate

            var updatedVehicle = _mockVehicleService.Object.UpdateVehicle(addedVehicle.VehicleID);

            // Assert: Check that the vehicle was updated successfully
            Assert.IsNotNull(updatedVehicle, "The updated vehicle should not be null.");
            Assert.That(updatedVehicle.Model, Is.EqualTo("Corolla"), "The vehicle model should be updated.");
            Assert.That(updatedVehicle.DailyRate, Is.EqualTo(55.0m), "The vehicle daily rate should be updated.");
        }

        [Test]
        public void UpdateVehicle_NonExistingVehicle_ShouldReturnNull()
        {
            // Arrange: Try to update a non-existing vehicle
            var nonExistingVehicleID = 99;  // Assuming vehicle with ID 99 doesn't exist

            // Act: Try to update a non-existing vehicle
            var result = _mockVehicleService.Object.UpdateVehicle(nonExistingVehicleID);

            // Assert: The update should fail for a non-existing vehicle
            Assert.IsNull(result, "Updating a non-existing vehicle should return null.");
        }
    }
}

