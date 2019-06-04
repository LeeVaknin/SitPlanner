using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SitPlanner.Models
{
    public class InviteeTable
    {
        public InviteeTable(Invitee invitee, Table table)
        {
            
        }

        public int Id { get; set; }

        public int InviteeId { get; set; }
        public virtual Invitee Invitee { get; set; }

        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        public int EventOptionId { get; set; }
        public virtual EventOption EventOption { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

    }
}
