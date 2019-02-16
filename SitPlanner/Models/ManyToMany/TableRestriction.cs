using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models.ManyToMany
{
    public class TableRestriction
    {
        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        public int RestrictionId { get; set; }
        public Restriction Restriction { get; set; }
    }
}
