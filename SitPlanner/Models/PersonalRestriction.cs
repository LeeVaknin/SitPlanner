using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class PersonalRestriction : Restriction
    {
        public int Id { get; set; }

        public List<Invitee> Invitees { get; set; }

    }
}
