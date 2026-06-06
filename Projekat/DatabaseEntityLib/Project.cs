﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
	public class Project
	{
		public int ID { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string? StartDate { get; set; }
		public string? EndDate { get; set; }
		public double? Budget { get; set; }
		public string Status { get; set; } = null!;

		public ICollection<WorkPackage> WorkPackages { get; set; } = new List<WorkPackage>();
	}
}