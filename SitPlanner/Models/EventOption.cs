
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class EventOption
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public List<InviteeCategory> InviteeCategories { get; set; }

        public List<PersonalRestriction> PersonalRestrictions { get; set; }

        public List<AccessibilityRestriction> AccessibilityRestrictions { get; set; }

    }
}
