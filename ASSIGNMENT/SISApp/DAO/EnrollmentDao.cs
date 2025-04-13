using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SISApp.Util;
using SISApp.Entities;


namespace SISApp.DAO
{
    public class EnrollmentDao : IEnrollmentDao<Enrollment>
    {
        SqlConnection sqlCon = DBConnUtil.GetConnection("AppSettings.json");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        //public Enrollment SaveEnrollment(Enrollment enrollment)
        //{
        //    try
        //    {
        //        cmd.Connection = sqlCon;
        //        cmd.CommandText = "INSERT INTO Enrollment (StudentID, CourseID, EnrollmentDate) VALUES (@StudentID, @CourseID, @EnrollmentDate)";
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@EnrollmentID", enrollment.EnrollmentID);
        //        cmd.Parameters.AddWithValue("@StudentID", enrollment.Student);
        //        cmd.Parameters.AddWithValue("@CourseID", enrollment.Course);
        //        cmd.Parameters.AddWithValue("@EnrollmentDate", enrollment.EnrollmentDate);
        //        if (sqlCon.State == System.Data.ConnectionState.Closed)
        //        {
        //            sqlCon.Open();
        //        }
        //        cmd.ExecuteNonQuery();
        //        return enrollment;
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine($"SQL Error: {ex.Message}");
        //        return null;
        //    }

        //}
    }
}
