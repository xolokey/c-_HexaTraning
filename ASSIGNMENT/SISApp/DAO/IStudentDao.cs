using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISApp.Entities;

namespace SISApp.DAO
{
    public interface IStudentDao<T>
    {
        T SaveStudent(T student);
        void EnrollStudentInCourse(Students student, Courses course);
        T UpdateStudentInfo(T student);
        void MakePayment(int studentId, decimal amount, DateOnly paymentDate);

        //T GetStudentDetails(T student);
    }
}
