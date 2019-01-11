using SitPlanner.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Table
    {
        public int Id { get; set; }

        public int NumberOfPeople { get; set; }

        public List<Restriction> Restrictions { get; set; }

        public List<Invitee> Invitees { get; set; }
    }
}
