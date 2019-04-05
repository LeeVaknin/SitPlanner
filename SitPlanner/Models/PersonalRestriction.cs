using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class PersonalRestriction
    {
        public int Id { get; set; }

        public int InviteeId { get; set; }
        public virtual Invitee Invitee { get; set; }

        public IList<Invitee> NotSittingTogether { get; set; }

        public IList<Invitee> SittingTogether { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public int EventOptionId { get; set; }
        public virtual EventOption EventOption { get; set; }

    }
}
