﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
    public class TeamMember
    {
        public int ID { get; set; }
        public int TeamID { get; set; }
        public int EmployeeID { get; set; }
        public string? RoleInTeam { get; set; }

        public Team? Team { get; set; }
        public Employee? Employee { get; set; }
    }
}