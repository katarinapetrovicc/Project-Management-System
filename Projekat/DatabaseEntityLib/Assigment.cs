﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
    public class Assignment
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int ActivityID { get; set; }
        public int AssignedDays { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Progress { get; set; }

        public Employee? Employee { get; set; }
        public Activity? Activity { get; set; }
    }
}