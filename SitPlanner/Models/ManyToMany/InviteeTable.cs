using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models.Enums
{
    public class InviteeTable
    {
        public int InviteeId { get; set; }
        public virtual Invitee Invitee { get; set; }

        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        public int ArrangementId { get; set; }
        public virtual Arrangement Arrangement { get; set; }

        
    }
}
