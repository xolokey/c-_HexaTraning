using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISApp.Entities
{
    public class Students
    {
        public int StudentID { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; } 
    }
}
