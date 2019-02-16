using SitPlanner.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class AccessibilityRestriction : Restriction
    {
        public int Id { get; set; }

        public TableType TableType { get; set; }
        
        public List<Table> Tables { get; set; }
    }
}
