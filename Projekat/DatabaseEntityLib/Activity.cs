﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
    public class Activity
    {
        public int ID { get; set; }
        public int TaskID { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? PlannedHours { get; set; }
        public decimal? ActualHours { get; set; }
        public string DatePerformed { get; set; } = null!;
        public byte[]? Attachment { get; set; }

        public Task? Task { get; set; }
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }

}