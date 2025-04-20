using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.Util;
using CarConnectApp.Entities;
using Microsoft.Data.SqlClient;
using CarConnectApp.Exception;

namespace CarConnectApp.DAO
{
    public class ReservationService : IReservationService<Reservation>
    {
        //Get Reservation by ID
        public Reservation GetReservationById(int reservationId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    Reservation reservation = null;
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Reservation WHERE ReservationID = @ReservationID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reservation = new Reservation
                                {
                                    ReservationID = reader.GetInt32(0),
                                    CustomerID = reader.GetInt32(1),
                                    VehicleID = reader.GetInt32(2),
                                    StartDate = reader.GetDateTime(3),
                                    EndDate = reader.GetDateTime(4),
                                    TotalCost = reader.GetDecimal(5),
                                    Status = reader.GetString(6)
                                };
                            }
                        }
                    }
                    return reservation;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine("Invalid Exception Error: " + ex.Message);
            }
            return null;
        }
        //To Get Reservation by Customer ID
        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    List<Reservation> reservations = new List<Reservation>();
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Reservation WHERE CustomerID = @CustomerID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Reservation reservation = new Reservation
                                {
                                    ReservationID = reader.GetInt32(0),
                                    CustomerID = reader.GetInt32(1),
                                    VehicleID = reader.GetInt32(2),
                                    StartDate = reader.GetDateTime(3),
                                    EndDate = reader.GetDateTime(4),
                                    TotalCost = reader.GetDecimal(5),
                                    Status = reader.GetString(6)
                                };
                                reservations.Add(reservation);
                            }
                        }
                    }
                    return reservations;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return null;
        }
        //To Create a Reservation
        public Reservation CreateReservation(Reservation reservation)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Reservation (CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status) VALUES (@CustomerID, @VehicleID, @StartDate, @EndDate, @TotalCost, @Status)", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", reservation.CustomerID);
                        cmd.Parameters.AddWithValue("@VehicleID", reservation.VehicleID);
                        cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                        cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                        cmd.Parameters.AddWithValue("@Status", reservation.Status);
                        cmd.ExecuteNonQuery();
                        return reservation;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (ReservationException ex)
            {
                Console.WriteLine("Reservation Exception Error: " + ex.Message);
            }
            return null;

        }
        //To Update Reservation
        public Reservation UpdateReservation(int reservationId, Reservation reservation)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Reservation SET CustomerID = @CustomerID, VehicleID = @VehicleID, StartDate = @StartDate, EndDate = @EndDate, TotalCost = @TotalCost, Status = @Status WHERE ReservationID = @ReservationID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        cmd.Parameters.AddWithValue("@CustomerID", reservation.CustomerID);
                        cmd.Parameters.AddWithValue("@VehicleID", reservation.VehicleID);
                        cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                        cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                        cmd.Parameters.AddWithValue("@Status", reservation.Status);
                        cmd.ExecuteNonQuery();
                        return reservation;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (ReservationException ex)
            {
                Console.WriteLine("Reservation Exception Error: " + ex.Message);
            }
            return null;
        }
        //To Cancel Reservation
        public bool CancelReservation(int reservationId)
        {
            try
            {
                using SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
                {
                    sqlCon.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Reservation WHERE ReservationID = @ReservationID", sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
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
