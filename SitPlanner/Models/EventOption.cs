using SitPlanner.Models.ManyToMany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class EventOption: Event
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        //Do we need it? or it will be passed in the constractor? 
        public virtual Event Event { get; set; }

        public int InviteeCategoryId { get; set; }
        public virtual InviteeCategory InviteeCategory { get; set; }



    }
}
