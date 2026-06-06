﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntityLib
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string? Logo { get; set; }

        public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    }
}