using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class PersonalRestriction : Restriction
    {
        public int Id { get; set; }

        // Think how to handle those lists
        public IList<Invitee> NotSittingTogether { get; set; }

        public IList<Invitee> SittingTogether { get; set; }

    }
}
