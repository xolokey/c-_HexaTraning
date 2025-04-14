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
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while processing the payment: {ex.Message}");
            }
        }

    }
}
