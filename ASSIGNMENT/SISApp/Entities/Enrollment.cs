using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISApp.Entities
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public Students? StudentID { get; set; }
        public Courses? CourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
