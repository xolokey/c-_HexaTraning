using System;
using System.Collections.Generic;
namespace SampleApp
{
    internal class StudentData
    {
        static void Main(string[] args)
        {
            Student student1 = new Student(100,"Sanjay","address","city","coursename");

            Console.WriteLine(student1.studentinfo());
            Console.WriteLine(student1.studentdetails());
        }
       
    }
}
