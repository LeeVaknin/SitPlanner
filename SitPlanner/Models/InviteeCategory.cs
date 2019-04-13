using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SitPlanner.Models
{
    
    public class InviteeCategory
    {
        public int Id { get; set; }

        [Key]
        public int InviteeId { get; set; }
        public virtual Invitee Invitee { get; set; }

        [Key]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int EventOptionId { get; set; }
        public virtual EventOption EventOption { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
