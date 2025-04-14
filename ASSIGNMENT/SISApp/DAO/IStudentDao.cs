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
        void GenerateEnrollmentReport(int courseID);

    }
}
