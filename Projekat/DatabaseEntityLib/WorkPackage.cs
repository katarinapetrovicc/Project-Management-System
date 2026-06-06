﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
    public class WorkPackage
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? PlannedDays { get; set; }
        public string? Priority { get; set; }
        public byte[]? Attachment { get; set; }

        public Project? Project { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}