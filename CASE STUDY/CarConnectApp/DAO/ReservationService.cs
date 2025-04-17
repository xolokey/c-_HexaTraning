using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectApp.Util;
using CarConnectApp.Entities;
using Microsoft.Data.SqlClient;

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

    }
}
