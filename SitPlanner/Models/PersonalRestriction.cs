using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class PersonalRestriction
    {
        public int Id { get; set; }

        //public int InviteeId { get; set; }
        public virtual Invitee Invitee { get; set; }

        public IList<Invitee> CannottSitWith { get; set; }

        public IList<Invitee> MustSitWith { get; set; }

        //public int EventId { get; set; }
        public virtual Event Event { get; set; }

        //public int EventOptionId { get; set; }
        public virtual EventOption EventOption { get; set; }

    }
}
