using System;
using SISApp.DAO;
using SISApp.Entities;
using SISApp.Util;
using SISApp.Exception;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SISApp.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create instances of StudentDao and sample data
            StudentDao studentDao = new StudentDao();

            // Sample student and course
            Students student1 = new Students
            {
                StudentID =102 ,
                FirstName = "Ajay",
                LastName = "Kumar",
                DateOfBirth = new DateTime(2004, 5, 16),
                Email = "ajay@gmail.com",
                PhoneNumber = "7575473467"
            };

            Courses course1 = new Courses
            {
                //CourseID = 101,
                CourseName = "C#",
                CourseCode = "CS101",
                InstructorName = "Ramesh"
            };

            // Update student information
            Console.Write("Enter 1.Save Student 2.Enrolling student 3.Update Student 4.Get By Id 5.GetAll:");
            var choice = Convert.ToInt32(Console.ReadLine());

            StudentDao studentDao1 = new StudentDao();
            switch (choice)
            {
                case 1:
                    //Save Student
                    Students student = new Students();
                    student.StudentID = 101;
                    student.FirstName = "Naveen";
                    student.LastName = "kumar";
                    student.DateOfBirth = new DateTime(2004, 5, 16);
                    student.Email = "naveen@gmail.com";
                    student.PhoneNumber = "7575473467";
                    var newStudent = studentDao1.SaveStudent(student);
                    Console.WriteLine(newStudent != null ? "Student is Saved" : "Error");
                    break;
                case 2:
                    //Enroll the student in a course
                    Console.WriteLine("Enrolling student in course...");
                    try
                    {
                        studentDao1.EnrollStudentInCourse(student1, course1);
                        Console.WriteLine($"Student {student1.FirstName} {student1.LastName} successfully enrolled in {course1.CourseName}.");
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case 3:
                    // Update Student Info
                    Students student2 = new Students();
                    student2.FirstName = "Praveen";
                    student2.LastName = "kumar";
                    student2.DateOfBirth = new DateTime(2004, 5, 16);
                    student2.Email = "praveen@gmail.com";
                    var updatedStudent = studentDao1.UpdateStudentInfo(student2);
                    Console.WriteLine(updatedStudent != null ? "Product is Updated" : "Error");
                    break;
                case 4:
                    try
                    {
                        studentDao.MakePayment(101, 2500.00m, DateOnly.FromDateTime(DateTime.Now));
                        Console.WriteLine("Payment made successfully.");
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;
                case 5:
                    break;





                    //case 5:
                    //    //Get student Details
                    //    var id1 = 101;
                    //    var studentById = studentDao1.GetStudentDetails(id1);
                    //    Console.WriteLine($"{studentById.StudentID}\t{studentById.FirstName}");
                    //    break;

            }
        }


            //    case 2:
            //        //Delete Product
            //        var productId = 1;
            //        var isDeleted = productInfoDao.DeleteProductInfo(1);
            //        Console.WriteLine(isDeleted ? "Product is Deleted" : "Error");
            //        break;
            //    case 3:
            //        //update product
            //        ProductInfo productInfo1 = new ProductInfo();
            //        productInfo1.ProductId = 1;
            //        productInfo1.ProductName = "Cookies";
            //        productInfo1.ListPrice = 12;
            //        productInfo1.ExpiryDate = DateTime.Parse("04-05-2026");
            //        var updatedProduct = productInfoDao.UpdateProductInfo(productInfo1);
            //        Console.WriteLine(updatedProduct != null ? "Product is Updated" : "Error");
            //        break;
            //    case 4:
            //        //Get product by id
            //        var id1 = 1;
            //        var productById = productInfoDao.GetProductInfoById(id1);
            //        Console.WriteLine($"{productById.ProductId}\t{productById.ProductName}");
            //        break;
            //    case 5:
            //        //Get All product
            //        var allProducts = productInfoDao.GetAllProductInfo();
            //        foreach (var product in allProducts)
            //        {
            //            Console.WriteLine($"{product.ProductId}\t{product.ProductName}");
            //        }
            //        break;
                  //}
            
            //else
            //{
            //    Console.WriteLine("Incorrect EmailId or Password");
            //}


        
    }
}
