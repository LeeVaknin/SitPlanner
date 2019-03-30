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
        
        public List<Table> Tables { get; set; }

        public List<Invitee> InviteesWithAccessibilityRestriction { get; set; }

    }
}
