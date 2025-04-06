using System;
namespace SampleApp
{
    class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string StudentCity { get; set; }
        public string StudentCourse { get; set; }

        public Student()
        {
            Console.WriteLine("Default constructor of Student");
        }
        public Student(int StudentId, string StudentName, string StudentAddress, string StudentCity, string StudentCourse)
        {
            this.StudentId = StudentId;
            this.StudentName = StudentName;
            this.StudentAddress = StudentAddress;
            this.StudentCity = StudentCity;
            this.StudentCourse = StudentCourse;
        }
        public string studentinfo()
        {
            return "getting student info";
        }
        public string studentdetails()
        {
            return $"Student Id={this.StudentId}\nStudent Name={this.StudentName}\nStudent Address={this.StudentAddress}\nStudent City={this.StudentCity}\nStudent Course={this.StudentCourse}";
        }
    }
}