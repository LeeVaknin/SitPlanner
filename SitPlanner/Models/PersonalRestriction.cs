using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SitPlanner.Models
{
    public class PersonalRestriction
    {
        public int Id { get; set; }

        public int MainInviteeId { get; set; }
        public virtual Invitee MainInvitee { get; set; }

        public int SecondaryInviteeId { get; set; }
        public virtual Invitee SecondaryInvitee { get; set; }

        public bool IsSittingTogether { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public int EventOptionId { get; set; }
        public virtual EventOption EventOption { get; set; }

    }
}
