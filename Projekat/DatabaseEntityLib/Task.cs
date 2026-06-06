﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
    public class Task
    {
        public int ID { get; set; }
        public int WorkPackageID { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? PlannedHours { get; set; }
        public int? ActualHours { get; set; }
        public string Deadline { get; set; } = null!;
        public string? Status { get; set; }

        public WorkPackage? WorkPackage { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}