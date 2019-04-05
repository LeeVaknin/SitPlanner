using SitPlanner.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class AccessibilityRestriction
    {
        public int Id { get; set; }

        public string AccessibilityRestrictionName { get; set; }

        public int InviteeId { get; set; }
        public virtual Invitee Invitee { get; set; }
        
        public List<Table> Tables { get; set; }

        public int EventOptionId { get; set; }
        public virtual EventOption EventOption { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

    }
}
