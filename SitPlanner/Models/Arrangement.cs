using SitPlanner.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Arrangement
    {
        public int Id { get; set; }

        // public List<Table> Tables { get; set; }

        public InviteeTable InviteeTable { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
