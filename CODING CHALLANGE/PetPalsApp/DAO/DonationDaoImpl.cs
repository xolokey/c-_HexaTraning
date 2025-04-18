using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PetPalsApp.Entity;
using PetPalsApp.Util;

namespace PetPalsApp.DAO
{
    public class DonationDaoImpl : IDonationDao
    {
        private int GetNextDonationId(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(donationid), 0) + 1 FROM donations", conn);
            return (int)cmd.ExecuteScalar();
        }

        public void InsertCashDonation(CashDonation donation)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();
                    int nextId = GetNextDonationId(conn);

                    string query = "INSERT INTO donations (donationid, donorname, donationtype, donationamount, donationitem, donationdate, shelterid) " +
                                   "VALUES (@id, @donorname, 'Cash', @amount, NULL, @donationdate, @shelterid)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", nextId);
                    cmd.Parameters.AddWithValue("@donorname", donation.DonorName);
                    cmd.Parameters.AddWithValue("@amount", donation.Amount);
                    cmd.Parameters.AddWithValue("@donationdate", donation.DonationDate);
                    cmd.Parameters.AddWithValue("@shelterid", 1);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Cash donation recorded successfully.");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("[DB ERROR - Cash Donation] " + ex.Message);
            }
        }


        public void InsertItemDonation(ItemDonation donation)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();
                    int nextId = GetNextDonationId(conn);

                    string query = "INSERT INTO donations (donationid, donorname, donationtype, donationamount, donationitem, donationdate, shelterid) " +
                                   "VALUES (@id, @donorname, 'Item', NULL, @item, @donationdate, @shelterid)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", nextId);
                    cmd.Parameters.AddWithValue("@donorname", donation.DonorName);
                    cmd.Parameters.AddWithValue("@item", donation.ItemType);
                    cmd.Parameters.AddWithValue("@donationdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@shelterid", 1);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Item donation recorded successfully.");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("[DB ERROR - Item Donation] " + ex.Message);
            }
        }


    }
}
