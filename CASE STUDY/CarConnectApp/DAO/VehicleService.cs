using System;
using System.Collections.Generic;
using CarConnectApp.Entities;
using CarConnectApp.Util;
using Microsoft.Data.SqlClient;

namespace CarConnectApp.DAO
{
    public class VehicleService : IVehicleService<Vehicle>
    {
        // To Get Vehicle by ID
        public Vehicle GetVehicleById(int vehicleId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle WHERE VehicleID = @VehicleID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Vehicle
                                {
                                    VehicleID = reader.GetInt32(0),
                                    Make = reader.GetString(1),
                                    Model = reader.GetString(2),
                                    Year = reader.GetDateTime(3), // Treat Year as an int
                                    Color = reader.GetString(4),
                                    RegistrationNumber = reader.GetString(5),
                                    Availability = reader.GetBoolean(6), // Use GetBoolean for Availability
                                    DailyRate = reader.GetDecimal(7)
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        // To Get Available Vehicles
        public List<Vehicle> GetAvailableVehicles()
        {
            List<Vehicle> availableVehicles = new List<Vehicle>();
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle WHERE Availability = 1", sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                availableVehicles.Add(new Vehicle
                                {
                                    VehicleID = reader.GetInt32(0),
                                    Make = reader.GetString(1),
                                    Model = reader.GetString(2),
                                    Year = reader.GetDateTime(3), // Extract the year from DateTime
                                    Color = reader.GetString(4),
                                    RegistrationNumber = reader.GetString(5),
                                    Availability = reader.GetByte(6) == 1, // Consistent handling
                                    DailyRate = reader.GetDecimal(7)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return availableVehicles;
        }

        // To Add Vehicle
        public Vehicle AddVehicle(Vehicle vehicle)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Vehicle (Make, Model, Year, Color, RegistrationNumber, Availability, DailyRate) VALUES (@Make, @Model, @Year, @Color, @RegistrationNumber, @Availability, @DailyRate)", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                        cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                        cmd.Parameters.AddWithValue("@Year", vehicle.Year); // Pass Year as an int
                        cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                        cmd.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                        cmd.Parameters.AddWithValue("@Availability", vehicle.Availability);
                        cmd.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
                        cmd.ExecuteNonQuery();
                    }
                }
                return vehicle;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }

        // To Update Vehicle
        public Vehicle UpdateVehicle(int vehicleID)
        {
            try
            {
                Vehicle vehicle = GetVehicleById(vehicleID);

                if (vehicle == null)
                {
                    Console.WriteLine("Vehicle not found with the given ID.");
                    return null;
                }

                // Prompt for updated values
                Console.WriteLine("\n--- Update Vehicle Details ---");

                Console.Write("Enter Make (" + vehicle.Make + "): ");
                string? make = Console.ReadLine();
                vehicle.Make = string.IsNullOrWhiteSpace(make) ? vehicle.Make : make;

                Console.Write("Enter Model (" + vehicle.Model + "): ");
                string? model = Console.ReadLine();
                vehicle.Model = string.IsNullOrWhiteSpace(model) ? vehicle.Model : model;

                Console.Write("Enter Year (" + vehicle.Year + "): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime year))
                {
                    vehicle.Year = year;
                }

                Console.Write("Enter Color (" + vehicle.Color + "): ");
                string? color = Console.ReadLine();
                vehicle.Color = string.IsNullOrWhiteSpace(color) ? vehicle.Color : color;

                Console.Write("Enter Registration Number (" + vehicle.RegistrationNumber + "): ");
                string? regNumber = Console.ReadLine();
                vehicle.RegistrationNumber = string.IsNullOrWhiteSpace(regNumber) ? vehicle.RegistrationNumber : regNumber;

                Console.Write("Is Available (true/false) (" + vehicle.Availability + "): ");
                if (bool.TryParse(Console.ReadLine(), out bool availability))
                {
                    vehicle.Availability = availability;
                }

                Console.Write("Enter Daily Rate (" + vehicle.DailyRate + "): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal dailyRate))
                {
                    vehicle.DailyRate = dailyRate;
                }

                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand(@"UPDATE Vehicle 
                        SET Make = @Make, Model = @Model, Year = @Year, Color = @Color, 
                            RegistrationNumber = @RegistrationNumber, Availability = @Availability, DailyRate = @DailyRate 
                        WHERE VehicleID = @VehicleID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);
                        cmd.Parameters.AddWithValue("@Make", vehicle.Make);
                        cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                        cmd.Parameters.AddWithValue("@Year", vehicle.Year); // Pass Year as an int
                        cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                        cmd.Parameters.AddWithValue("@RegistrationNumber", vehicle.RegistrationNumber);
                        cmd.Parameters.AddWithValue("@Availability", vehicle.Availability);
                        cmd.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Vehicle updated successfully.");
                            return vehicle;
                        }
                        else
                        {
                            Console.WriteLine("Update failed.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }

            return null;
        }

        // To Delete Vehicle
        public bool RemoveVehicle(int vehicleID)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Vehicle WHERE VehicleID = @VehicleID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@VehicleID", vehicleID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return false;
        }

        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicleList = new List<Vehicle>();
            using SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json");
            {
                conn.Open();
                string query = "SELECT * FROM Vehicle";
                using SqlCommand cmd = new SqlCommand(query, conn);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle
                    {
                        VehicleID = reader.GetInt32(0),
                        Make = reader.GetString(1),
                        Model = reader.GetString(2),
                        Year = reader.GetDateTime(3),
                        Color = reader.GetString(4),
                        RegistrationNumber = reader.GetString(5),
                        Availability = reader.GetByte(6) == 1,  //  Safely converting from byte to bool
                        DailyRate = reader.GetDecimal(7)
                    };
                    vehicleList.Add(vehicle);
                }
            }
            return vehicleList;
        }


    }
}
