using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models.Enums
{
    public class InviteeRestriction
    {
        public int InviteeId { get; set; }
        public virtual Invitee Invitee { get; set; }

        public int RestrictionId { get; set; }
        public Restriction Restriction { get; set; }
    }
}
