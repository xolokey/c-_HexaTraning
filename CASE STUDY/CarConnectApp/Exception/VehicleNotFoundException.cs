﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.Exception
{
    public class VehicleNotFoundException : System.Exception
    {
        public VehicleNotFoundException (string message) : base(message){ }
    }
    
}
