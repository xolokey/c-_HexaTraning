﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.Entities
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int CustomerID { get; set; }
        public int VehicleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public string? Status { get; set; }

        public decimal CalculateTotalCost(decimal dailyRate)
        {
            int totalDays = (EndDate - StartDate).Days;
            totalDays = totalDays > 0 ? totalDays : 1;
            TotalCost = totalDays * dailyRate;

            return TotalCost;
        }


    }

}
