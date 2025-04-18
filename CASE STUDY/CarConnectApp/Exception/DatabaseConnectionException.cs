using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.Exception
{
    public class DatabaseConnectionException : System.Exception
    {
        public DatabaseConnectionException(string message) : base(message) { }
    }
  
}
