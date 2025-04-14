using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SISApp.Util;
using SISApp.Entities;

namespace SISApp.DAO
{
    public class PaymentDao : IPaymentsDao<Payments>
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        //public Payments SavePayment(Payments payment)
        //{
        //    try
        //    {
        //        cmd.Connection = sqlCon;
        //        cmd.CommandText = "INSERT INTO Payments (StudentID, Amount, PaymentDate) VALUES (@StudentID, @Amount, @PaymentDate)";
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@PaymentID", payment.PaymentID);
        //        cmd.Parameters.AddWithValue("@StudentID", payment.StudentID);
        //        cmd.Parameters.AddWithValue("@Amount", payment.Amount);
        //        cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);

        //        if (sqlCon.State == System.Data.ConnectionState.Closed)
        //        {
        //            sqlCon.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        return payment;
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine($"SQL Error: {ex.Message}");
        //        return null;
        //    }

        //}
        public void RecordPayment(int studentId, decimal amount, DateTime paymentDate)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection("AppSettings.json"))
                {
                    conn.Open();

                    // Check if the student exists
                    string checkStudentSql = "SELECT COUNT(*) FROM Students WHERE StudentID = @StudentID";
                    using (SqlCommand checkCmd = new SqlCommand(checkStudentSql, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@StudentID", studentId);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            Console.WriteLine("Student not found.");
                            return;
                        }
                    }

                    // Insert payment
                    string insertPaymentSql = @"INSERT INTO Payments (StudentID, Amount, PaymentDate) 
                                        VALUES (@StudentID, @Amount, @PaymentDate)";
                    using (SqlCommand insertCmd = new SqlCommand(insertPaymentSql, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@StudentID", studentId);
                        insertCmd.Parameters.AddWithValue("@Amount", amount);
                        insertCmd.Parameters.AddWithValue("@PaymentDate", paymentDate);

                        insertCmd.ExecuteNonQuery();
                    }

                    // Update outstanding balance
                    //string updateBalanceSql = @"UPDATE Students SET OutstandingBalance = OutstandingBalance - @Amount 
                    //                    WHERE StudentID = @StudentID";
                    //using (SqlCommand updateCmd = new SqlCommand(updateBalanceSql, conn))
                    //{
                    //    updateCmd.Parameters.AddWithValue("@Amount", amount);
                    //    updateCmd.Parameters.AddWithValue("@StudentID", studentId);

                    //    updateCmd.ExecuteNonQuery();
                    //}

                    //Console.WriteLine("Payment recorded successfully and outstanding balance updated.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while processing the payment: {ex.Message}");
            }
        }

    }
}
