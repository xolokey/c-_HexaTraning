using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.Entities;
using CarConnectApp.Util;
using Microsoft.Data.SqlClient;

namespace CarConnectApp.DAO
{
    public class VehicleService : IVehicleService<Vehicle>

    {
        //To Get Vehicle by ID
        public Vehicle GetVehicleById(int vehicleId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    Vehicle vehicle = null;
                    sqlCon.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Vehicle WHERE VehicleID = @VehicleID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@VehicleID", vehicleId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                vehicle = new Vehicle
                                {
                                    VehicleID = reader.GetInt32(0),
                                    Make = reader.GetString(1),
                                    Model = reader.GetString(2),
                                    Year = reader.GetInt32(3),
                                    Color = reader.GetString(4),
                                    RegistrationNumber = reader.GetString(5),
                                    Availability = reader.GetBoolean(6),
                                    DailyRate = reader.GetDecimal(7)

                                };
                            }
                        }
                    }
                    return vehicle;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;

        }
        //To get avilable vehicles
        // To get available vehicles
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
                                Vehicle vehicle = new Vehicle
                                {
                                    VehicleID = reader.GetInt32(0),
                                    Make = reader.GetString(1),
                                    Model = reader.GetString(2),
                                    Year = reader.GetDateTime(3).Year, // Extract the year from DateTime
                                    Color = reader.GetString(4),
                                    RegistrationNumber = reader.GetString(5),
                                    Availability = reader.GetByte(6) == 1, // Convert byte to bool
                                    DailyRate = reader.GetDecimal(7)
                                };
                                availableVehicles.Add(vehicle);
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

        //To Add Vehicle
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
                        cmd.Parameters.AddWithValue("@Year", vehicle.Year);
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
        //To Update Vehicle
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
                vehicle.Make = Console.ReadLine();

                Console.Write("Enter Model (" + vehicle.Model + "): ");
                vehicle.Model = Console.ReadLine();

                Console.Write("Enter Year (" + vehicle.Year + "): ");
                vehicle.Year = int.Parse(Console.ReadLine());

                Console.Write("Enter Color (" + vehicle.Color + "): ");
                vehicle.Color = Console.ReadLine();

                Console.Write("Enter Registration Number (" + vehicle.RegistrationNumber + "): ");
                vehicle.RegistrationNumber = Console.ReadLine();

                Console.Write("Is Available (true/false) (" + vehicle.Availability + "): ");
                vehicle.Availability = bool.Parse(Console.ReadLine());

                Console.Write("Enter Daily Rate (" + vehicle.DailyRate + "): ");
                vehicle.DailyRate = decimal.Parse(Console.ReadLine());

                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using SqlCommand cmd = new SqlCommand(@"UPDATE Vehicle 
                SET Make = @Make, Model = @Model, Year = @Year, Color = @Color, 
                    RegistrationNumber = @RegistrationNumber, Availability = @Availability, DailyRate = @DailyRate 
                WHERE VehicleID = @VehicleID", sqlCon)
                    {
                        Parameters =
                {
                    new SqlParameter("@VehicleID", vehicle.VehicleID),
                    new SqlParameter("@Make", vehicle.Make),
                    new SqlParameter("@Model", vehicle.Model),
                    new SqlParameter("@Year", vehicle.Year),
                    new SqlParameter("@Color", vehicle.Color),
                    new SqlParameter("@RegistrationNumber", vehicle.RegistrationNumber),
                    new SqlParameter("@Availability", vehicle.Availability),
                    new SqlParameter("@DailyRate", vehicle.DailyRate)
                }
                    };

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
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format.");
            }

            return null;
        }
        //To Delete Vehicle
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


    }
}
