using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentInformationSystem
{
    // Custom Exceptions
    public class DuplicateEnrollmentException : Exception { public DuplicateEnrollmentException(string message) : base(message) { } }
    public class CourseNotFoundException : Exception { public CourseNotFoundException(string message) : base(message) { } }
    public class StudentNotFoundException : Exception { public StudentNotFoundException(string message) : base(message) { } }
    public class TeacherNotFoundException : Exception { public TeacherNotFoundException(string message) : base(message) { } }
    public class PaymentValidationException : Exception { public PaymentValidationException(string message) : base(message) { } }
    public class InvalidStudentDataException : Exception { public InvalidStudentDataException(string message) : base(message) { } }
    public class InvalidCourseDataException : Exception { public InvalidCourseDataException(string message) : base(message) { } }
    public class InvalidEnrollmentDataException : Exception { public InvalidEnrollmentDataException(string message) : base(message) { } }
    public class InvalidTeacherDataException : Exception { public InvalidTeacherDataException(string message) : base(message) { } }
    public class InsufficientFundsException : Exception { public InsufficientFundsException(string message) : base(message) { } }

    // Student Class
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        private List<Enrollment> enrollments = new List<Enrollment>();
        private List<Payment> payments = new List<Payment>();

        public void EnrollInCourse(Course course)
        {
            if (enrollments.Any(e => e.CourseID == course.CourseID))
                throw new DuplicateEnrollmentException("Student is already enrolled in this course.");

            var enrollment = new Enrollment
            {
                EnrollmentID = new Random().Next(1000, 9999),
                StudentID = this.StudentID,
                CourseID = course.CourseID,
                EnrollmentDate = DateTime.Now
            };
            enrollments.Add(enrollment);
            course.AddEnrollment(enrollment);
        }

        public void UpdateStudentInfo(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            if (dateOfBirth > DateTime.Now || !email.Contains("@"))
                throw new InvalidStudentDataException("Invalid student data provided.");

            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void MakePayment(decimal amount, DateTime paymentDate)
        {
            if (amount <= 0 || paymentDate > DateTime.Now)
                throw new PaymentValidationException("Invalid payment amount or date.");

            payments.Add(new Payment { PaymentID = new Random().Next(1000, 9999), Student = this, Amount = amount, PaymentDate = paymentDate });
        }

        public void DisplayStudentInfo()
        {
            Console.WriteLine($"StudentID: {StudentID}, StudentName: {FirstName} {LastName}, DOB: {DateOfBirth.ToShortDateString()}, Email: {Email}, Phone: {PhoneNumber}");
        }

        public List<Enrollment> GetEnrolledCourses() => enrollments;
        public List<Payment> GetPaymentHistory() => payments;
    }

    // Course Class
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
        public Teacher? AssignedTeacher { get; set; }

        private List<Enrollment> enrollments = new List<Enrollment>();

        public void AssignTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new TeacherNotFoundException("Teacher not found.");

            AssignedTeacher = teacher;
            teacher.AddAssignedCourse(this);
        }

        public void UpdateCourseInfo(string courseCode, string courseName, string instructor)
        {
            if (string.IsNullOrWhiteSpace(courseCode) || string.IsNullOrWhiteSpace(instructor))
                throw new InvalidCourseDataException("Invalid course data.");

            CourseCode = courseCode;
            CourseName = courseName;
            InstructorName = instructor;
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseID}, CourseCode: {CourseCode}, CourseName: {CourseName}, Instructor: {InstructorName}");
        }

        public List<Enrollment> GetEnrollments() => enrollments;
        public Teacher? GetTeacher() => AssignedTeacher;
        public void AddEnrollment(Enrollment enrollment) => enrollments.Add(enrollment);
        public int GetTotalEnrollments() => enrollments.Count;

        public decimal GetTotalPayments(List<Student> students)
        {
            return enrollments.Sum(e => e.GetStudent(students)?.GetPaymentHistory().Sum(p => p.Amount) ?? 0);
        }
    }

    // Enrollment Class
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Student? GetStudent(List<Student> students) => students.FirstOrDefault(s => s.StudentID == StudentID);
        public Course? GetCourse(List<Course> courses) => courses.FirstOrDefault(c => c.CourseID == CourseID);
    }

    // Teacher Class
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        private List<Course> assignedCourses = new List<Course>();

        public void UpdateTeacherInfo(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(email))
                throw new InvalidTeacherDataException("Invalid teacher data.");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void DisplayTeacherInfo()
        {
            Console.WriteLine($"TeacherID: {TeacherID}, TeacherName: {FirstName} {LastName}, Email: {Email}");
        }

        public List<Course> GetAssignedCourses() => assignedCourses;
        public void AddAssignedCourse(Course course) => assignedCourses.Add(course);
    }

    // Payment Class
    public class Payment
    {
        public int PaymentID { get; set; }
        public Student? Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Student? GetStudent() => Student;
        public decimal GetPaymentAmount() => Amount;
        public DateTime GetPaymentDate() => PaymentDate;
    }

    // SIS Class
    public class SIS
    {
        private List<Student> allStudents = new List<Student>();
        private List<Course> allCourses = new List<Course>();
        private List<Teacher> allTeachers = new List<Teacher>();

        public void EnrollStudentInCourse(Student student, Course course)
        {
            if (student == null) throw new StudentNotFoundException("Student not found.");
            if (course == null) throw new CourseNotFoundException("Course not found.");

            student.EnrollInCourse(course);
            if (!allStudents.Contains(student))
                allStudents.Add(student);
        }

        public void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            if (teacher == null) throw new TeacherNotFoundException("Teacher not found.");
            if (course == null) throw new CourseNotFoundException("Course not found.");

            course.AssignTeacher(teacher);
        }

        public void RecordPayment(Student student, decimal amount, DateTime paymentDate)
        {
            if (student == null) throw new StudentNotFoundException("Student not found.");
            student.MakePayment(amount, paymentDate);
        }

        public void GenerateEnrollmentReport(Course course)
        {
            Console.WriteLine($"\nEnrollment Report for {course.CourseName}:");
            foreach (var enrollment in course.GetEnrollments())
            {
                Console.WriteLine($"Enrollment ID: {enrollment.EnrollmentID}, Student ID: {enrollment.StudentID}, EnrollmentDate: {enrollment.EnrollmentDate}");
            }
        }

        public void GeneratePaymentReport(Student student)
        {
            Console.WriteLine($"\nPayment Report for {student.FirstName} {student.LastName}:");
            foreach (var payment in student.GetPaymentHistory())
            {
                Console.WriteLine($"Payment ID: {payment.PaymentID}, Amount: {payment.Amount}, PaymentDate: {payment.PaymentDate}");
            }
        }

        public void CalculateCourseStatistics(Course course)
        {
            var totalEnrollments = course.GetTotalEnrollments();
            var totalPayments = course.GetTotalPayments(allStudents);
            Console.WriteLine($"\nCourse Statistics for {course.CourseName}:");
            Console.WriteLine($"Total Enrollments: {totalEnrollments}");
            Console.WriteLine($"Total Payments: {totalPayments:C}");
        }

        public List<Enrollment> GetEnrollmentsForStudent(Student student) => student.GetEnrolledCourses();
        public List<Course> GetCoursesForTeacher(Teacher teacher) => teacher.GetAssignedCourses();
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Student student1 = new Student
                { 
                    StudentID = 101, 
                    FirstName = "Ajay", 
                    LastName = "Kumar", 
                    DateOfBirth = new DateTime(2004, 5, 15), 
                    Email = "ajay@gmail.com",
                    PhoneNumber = "9786579435" 
                };
                Student student2 = new Student
                {
                    StudentID = 102,
                    FirstName = "Vijay",
                    LastName = "Sharma",
                    DateOfBirth = new DateTime(2003, 8, 20),
                    Email="vijay@gmail.com",
                    PhoneNumber = "9876543210"
                };
                Course course1 = new Course 
                { 
                    CourseID = 201, 
                    CourseName = "C#", 
                    CourseCode = "CS101", 
                    InstructorName = "Manoj"
                };
                Course course2 = new Course
                {
                    CourseID = 202,
                    CourseName = "Java",
                    CourseCode = "CS102",
                    InstructorName = "Ravi"
                };
                Teacher teacher1 = new Teacher 
                { 
                    TeacherID = 301,
                    FirstName = "Soyeb",
                    LastName = "Ganchi",
                    Email = "soyeb@gmail.com"
                };
                Teacher teacher2 = new Teacher
                {
                    TeacherID = 302,
                    FirstName = "Kavitha",
                    LastName = "Suresh",
                    Email="kavitha@gmaail.com"

                };


                SIS sis = new SIS();

                sis.EnrollStudentInCourse(student1, course1);
                sis.AssignTeacherToCourse(teacher1, course1);
                sis.RecordPayment(student1, 3500.00m, DateTime.Now);
                sis.RecordPayment(student1, 2500.00m, DateTime.Now);
                student1.EnrollInCourse(course2);

                sis.EnrollStudentInCourse(student2, course2);
                sis.AssignTeacherToCourse(teacher2, course2); 
                sis.RecordPayment(student2, 4500.00m, DateTime.Now);

                //student1.GetPaymentHistory();
                

                //Display student and course information
                Console.WriteLine("Student Information...");
                student1.DisplayStudentInfo();
                student2.DisplayStudentInfo();
                Console.WriteLine();

                //Update student information
                Console.WriteLine("Updated Student1 Information...");
                student1.UpdateStudentInfo(firstName:"Sanjay",lastName:"Kumar",dateOfBirth:new DateTime(2004,5,15),email:"sanjay@gmail.com",phoneNumber: "9876543210");
                student1.DisplayStudentInfo();
                Console.WriteLine();

                Console.WriteLine("Course Information...");
                course1.DisplayCourseInfo();
                course2.DisplayCourseInfo();
                Console.WriteLine();

                Console.WriteLine("Teacher Information...");
                teacher1.DisplayTeacherInfo();
                teacher2.DisplayTeacherInfo();
                Console.WriteLine();

                //Display Statistics--Course1
                Console.WriteLine("Course 1 Statistics Report...");
                sis.GenerateEnrollmentReport(course1);
                sis.CalculateCourseStatistics(course1);
                Console.WriteLine();

                //Display Statistics--Course2
                Console.WriteLine("Course 2 Statistics Report...");
                sis.GenerateEnrollmentReport(course2);
                sis.CalculateCourseStatistics(course2);
                Console.WriteLine();

                //Payment Report
                Console.WriteLine("Student Payment Report...");
                sis.GeneratePaymentReport(student1);
                sis.GeneratePaymentReport(student2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}