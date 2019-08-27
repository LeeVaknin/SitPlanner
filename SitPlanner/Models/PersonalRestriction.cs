using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SitPlanner.Models
{
    public class PersonalRestriction
    {
        public int Id { get; set; }

        [Display(Name = "First Invitee")]
        public int MainInviteeId { get; set; }
        [Display(Name = "First Invitee")]
        public virtual Invitee MainInvitee { get; set; }

        [Display(Name = "Second Invitee")]
        public int SecondaryInviteeId { get; set; }
        [Display(Name = "Second Invitee")]
        public virtual Invitee SecondaryInvitee { get; set; }

        [Display(Name = "Is sitting together?")]
        public bool IsSittingTogether { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
